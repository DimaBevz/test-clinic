using Application.Common.Interfaces.Repositories;
using Application.Physician.DTOs.RequestDTOs;
using Application.Physician.DTOs.ResponseDTOs;
using Infrastructure.Persistence.Entities;
using Infrastructure.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Application.Admin.DTOs.Request;
using Application.Admin.DTOs.Response;
using Application.Common.Enums;

namespace Infrastructure.Persistence.Repositories
{
    internal class PhysicianRepository : IPhysicianRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PhysicianRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetPhysicianDataDto> GetPhysicianDataAsync(Guid id)
        {
            var physicianData = await _dbContext.PhysicianData
                .Include(p => p.Positions)
                .SingleAsync(x => x.UserId == id);

            var dto = physicianData.ToDto();
                
            return dto;
        }

        public async Task<GetPhysicianDataDto> AddPhysicianDataAsync(AddPhysicianDto physicianDto)
        {
            var physicianData = physicianDto.ToEntity();

            await _dbContext.PhysicianData.AddAsync(physicianData);
            await _dbContext.SaveChangesAsync();

            var dto = physicianData.ToDto();
            return dto;
        }

        public async Task<GetPhysicianDataDto> UpdatePhysicianAsync(UpdatePhysicianDto updatePhysicianDto)
        {
            var currentEntityToUpdate = await _dbContext.PhysicianData
                .Include(p => p.Positions)
                .SingleAsync(p => p.UserId == updatePhysicianDto.Id);

            updatePhysicianDto.ToCurrentEntity(currentEntityToUpdate);

            if (updatePhysicianDto.PositionIds is not null)
            {
                var positions = await _dbContext.Positions.Where(p => updatePhysicianDto.PositionIds.Contains(p.Id)).ToListAsync();
                currentEntityToUpdate.Positions = positions;
            }

            _dbContext.PhysicianData.Update(currentEntityToUpdate);
            await _dbContext.SaveChangesAsync();

            var dto = currentEntityToUpdate.ToDto();
            return dto;
        }

        public async Task<ApprovedPhysicianDto?> ApprovePhysicianAsync(ApprovePhysicianDto physicianDto, CancellationToken cancellationToken)
        {
            var physician =
                await _dbContext.PhysicianData.FirstOrDefaultAsync(p => p.UserId == physicianDto.Id, cancellationToken);

            if (physician is null)
            {
                return null;
            }

            var adminMessage =
                await _dbContext.ApproveDocumentMessages.FirstOrDefaultAsync(m => m.PhysicianDataId == physicianDto.Id,
                    cancellationToken);

            if (adminMessage is not null)
            {
                _dbContext.ApproveDocumentMessages.Remove(adminMessage);
            }

            physician.IsApproved = true;
            _dbContext.PhysicianData.Update(physician);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var result = new ApprovedPhysicianDto(physicianDto.Id, true);
            return result;
        }

        public async Task<DeclinedPhysicianDto?> DeclinePhysicianAsync(DeclinePhysicianDto physicianDto, CancellationToken cancellationToken)
        {
            var physician =
                await _dbContext.PhysicianData.FirstOrDefaultAsync(p => p.UserId == physicianDto.Id, cancellationToken);

            if (physician is null)
            {
                return null;
            }

            var adminMessage =
                await _dbContext.ApproveDocumentMessages.FirstOrDefaultAsync(m => m.PhysicianDataId == physicianDto.Id,
                    cancellationToken);

            if (adminMessage is not null)
            {
                adminMessage.Message = physicianDto.Message;
                _dbContext.ApproveDocumentMessages.Update(adminMessage);
            }
            else
            {
                var newAdminMessage = new ApproveDocumentMessage
                {
                    PhysicianDataId = physicianDto.Id,
                    Message = physicianDto.Message
                };
                await _dbContext.ApproveDocumentMessages.AddAsync(newAdminMessage, cancellationToken);
            }

            physician.IsApproved = false;
            _dbContext.PhysicianData.Update(physician);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var result = new DeclinedPhysicianDto(physicianDto.Id, false, physicianDto.Message);
            return result;
        }

        public async Task<GetPhysiciansDto> GetPhysiciansAsync()
        {
            var physicianDtoItems = await _dbContext.PhysicianData
                .Include(p => p.Positions)
                .Include(p => p.Comments)
                .Include(p => p.User)
                .ThenInclude(u => u!.UserPhotoData)
                .Where(p => p.IsApproved)
                .Select(p => p.ToItemDto())
                .ToListAsync();

            var dto = new GetPhysiciansDto(physicianDtoItems);
            return dto;
        }

        public async Task<GetUnapprovedPhysiciansDto> GetAllUnapprovedPhysiciansAsync(CancellationToken cancellationToken)
        {
            var unapprovedPhysicians = await _dbContext.PhysicianData
                .Include(p => p.User)
                .ThenInclude(u => u.Documents)
                .Include(u => u.User)
                .ThenInclude(u => u.UserPhotoData)
                .Include(p => p.ApproveDocumentMessage)
                .Where(p => !p.IsApproved)
                .Select(p => new GetPhysicianItemDto(p.UserId, p.User.LastName, p.User.FirstName, p.User.Patronymic,
                    p.User.UserPhotoData.PresignedUrl ?? "",
                    p.User.Documents.Count(d =>
                        d.Type == DocumentType.Certificate || d.Type == DocumentType.License ||
                        d.Type == DocumentType.Diploma),
                    p.ApproveDocumentMessage.Message ?? ""))
                .ToListAsync(cancellationToken);

            var result = new GetUnapprovedPhysiciansDto(unapprovedPhysicians);

            return result;
        }

        public async Task<GetPaginatedPhysiciansDto<PhysicianItemDto>> GetPhysiciansAsync(GetPhysiciansByParamsDto paramsDto)
        {
            var (page, limit, isAscending, isApproved, searchValue, sortField, sex, rating, experience) = paramsDto;

            var query = _dbContext.PhysicianData
                .Include(p => p.Positions)
                .Include(p => p.Comments)
                .Include(p => p.User)
                .ThenInclude(u => u!.UserPhotoData)
                .AsQueryable();

            if (isApproved is not null)
            {
                query = query.Where(pd => pd.IsApproved == isApproved);
            }

            if (sex is not null && sex.Count > 0)
            {
                var selectedGenders = sex.Select(s => Enum.GetName(s)).ToList();
                query = query.Where(pd => selectedGenders.Contains(pd.User!.Sex));
            }

            if (experience.HasValue && experience.Value != 0)
            {
                var currentDate = DateOnly.FromDateTime(DateTime.UtcNow);

                query = query.Where(pd =>
                    pd.Experience.HasValue &&
                    (currentDate.Year - pd.Experience.Value.Year - 
                    (currentDate.Month < pd.Experience.Value.Month 
                    || (currentDate.Month == pd.Experience.Value.Month && currentDate.Day < pd.Experience.Value.Day) ? 1 : 0)
                    ) >= experience.Value);
            }

            if (rating is not null && rating.Count > 0)
            {
                foreach (var r in rating)
                {
                    switch (r)
                    {
                        case EvaluationType.None:
                            query = query.Where(pd => pd.Rating == 0);
                            break;
                        case EvaluationType.Normally:
                            query = query.Where(pd => pd.Rating >= 3 && pd.Rating < 4);
                            break;
                        case EvaluationType.Good:
                            query = query.Where(pd => pd.Rating >= 4 && pd.Rating < 4.7);
                            break;
                        case EvaluationType.VeryGood:
                            query = query.Where(pd => pd.Rating >= 4.7);
                            break;
                    }
                }
            }

            if (searchValue is not null)
            {
                query = query
                .Where(p => 
                    EF.Functions.Like(p.User!.FirstName, $"%{searchValue}%") ||
                    EF.Functions.Like(p.User.LastName, $"%{searchValue}%") ||
                    EF.Functions.Like(p.User.Patronymic, $"%{searchValue}%") ||
                    p.PhysicianSpecialties.Where(s => EF.Functions.Like(s.Position.Specialty, $"%{searchValue}%")).Any()
                );
            }

            if (sortField is not null)
            {
                var sort = Enum.GetName(sortField.Value);
                var parameter = Expression.Parameter(typeof(PhysicianData), "t");

                var property = Expression.Property(parameter, sort!);
                var objectProperty = Expression.TypeAs(property, typeof(object));

                var sortSelector = Expression.Lambda<Func<PhysicianData, object>>(objectProperty, parameter);

                if (sort != "Experience")
                {
                    query = isAscending
                    ? query.OrderBy(sortSelector)
                    : query.OrderByDescending(sortSelector);
                }
                else
                {
                    query = isAscending
                    ? query.OrderByDescending(sortSelector)
                    : query.OrderBy(sortSelector);
                }
            }

            var totalCount = await query.CountAsync();

            query = query
                .Skip(limit * (page - 1))
                .Take(limit);

            var physicianItemDtos = await query
                .Select(pd => pd.ToItemDto())
                .ToListAsync();

            var getPhysiciansDto = new GetPaginatedPhysiciansDto<PhysicianItemDto> 
            {
                Physicians = physicianItemDtos,
                TotalCount = totalCount
            };

            return getPhysiciansDto;
        }
    }
}
