using Application.Common.Interfaces.Repositories;
using Application.Physician.DTOs.RequestDTOs;
using Application.Physician.DTOs.ResponseDTOs;
using Infrastructure.Persistence.Entities;
using Infrastructure.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Xml.Linq;

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

        public async Task<GetPhysiciansDto> GetPhysiciansAsync()
        {
            var physicianDtoItems = await _dbContext.PhysicianData
                .Include(p => p.Positions)
                .Include(p => p.Comments)
                .Include(p => p.User)
                .ThenInclude(u => u!.UserPhotoData)
                .Select(p => p.ToItemDto())
                .ToListAsync();

            var dto = new GetPhysiciansDto(physicianDtoItems);
            return dto;
        }

        public async Task<GetPaginatedPhysiciansDto<PhysicianItemDto>> GetPhysiciansAsync(GetPhysiciansByParamsDto paramsDto)
        {
            var (page, limit, isAscending, isApproved, physicianName, positionId, sortField) = paramsDto;

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

            if (positionId is not null)
            {
                var position = await _dbContext.Positions.SingleAsync(p => p.Id == positionId);
                query = query.Where(pd => pd.Positions!.Contains(position));
            }

            if (physicianName is not null)
            {
                query = query
                .Where(p => 
                    EF.Functions.Like(p.User!.FirstName, $"%{physicianName}%") ||
                    EF.Functions.Like(p.User.LastName, $"%{physicianName}%") ||
                    EF.Functions.Like(p.User.Patronymic, $"%{physicianName}%")
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

            var physicianItemDtos = await query
                .Skip(limit * (page - 1))
                .Take(limit)
                .Select(pd => pd.ToItemDto())
                .ToListAsync();

            var totalCount = await _dbContext.PhysicianData.CountAsync();

            var getPhysiciansDto = new GetPaginatedPhysiciansDto<PhysicianItemDto> 
            {
                Physicians = physicianItemDtos,
                TotalCount = totalCount
            };

            return getPhysiciansDto;
        }
    }
}
