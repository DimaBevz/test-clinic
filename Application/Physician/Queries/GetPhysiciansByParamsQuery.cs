using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Physician.DTOs.RequestDTOs;
using Application.Physician.DTOs.ResponseDTOs;
using Mediator;

namespace Application.Physician.Queries
{
    public record GetPhysiciansByParamsQuery(GetPhysiciansByParamsDto Dto) : IQuery<GetPaginatedPhysiciansDto<PhysicianItemDto>>;

    public class GetPhysiciansByParamsQueryHandler : IQueryHandler<GetPhysiciansByParamsQuery, GetPaginatedPhysiciansDto<PhysicianItemDto>>
    {
        private readonly IPhysicianRepository _physicianRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFileService _fileService;

        public GetPhysiciansByParamsQueryHandler(IPhysicianRepository physicianRepository, IUserRepository userRepository, IFileService fileService)
        {
            _physicianRepository = physicianRepository;
            _userRepository = userRepository;
            _fileService = fileService;
        }

        public async ValueTask<GetPaginatedPhysiciansDto<PhysicianItemDto>> Handle(GetPhysiciansByParamsQuery request, CancellationToken cancellationToken)
        {
            var paginatedPhysiciansDto = await _physicianRepository.GetPhysiciansAsync(request.Dto);
            var updatedPhysicians = new List<PhysicianItemDto>();

            foreach (var physician in paginatedPhysiciansDto.Physicians)
            {
                if (physician?.PhotoExpiration is not null && physician.PhotoExpiration < DateTime.UtcNow)
                {
                    var resultDto = await _fileService.UpdatePresignedLinkAsync(physician.PhotoObjectKey!);
                    var userPhoto = await _userRepository.UpdateUserPhotoAsync(physician.Id, resultDto);

                    updatedPhysicians.Add(physician with { PhotoUrl = userPhoto?.PresignedUrl });
                }
                else
                {
                    updatedPhysicians.Add(physician!);
                }
            }

            paginatedPhysiciansDto.Physicians = updatedPhysicians;

            return paginatedPhysiciansDto;
        }
    }
}
