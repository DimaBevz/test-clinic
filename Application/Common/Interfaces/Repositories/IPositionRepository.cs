using Application.Position.DTOs.RequestDTOs;
using Application.Position.DTOs.ResponseDTOs;

namespace Application.Common.Interfaces.Repositories
{
    public interface IPositionRepository
    {
        public Task<GetPositionsDto> GetPositionsAsync();
        public Task<PositionDto> GetPositionByIdAsync(Guid id);

        public Task<PositionDto> AddPositionAsync(AddPositionDto dto);
        public Task<GetPositionsDto> AddPositionRangeAsync(AddPositionListDto dtos);

        public Task<PositionDto> RemovePositionAsync(Guid positionId);
        public Task RemovePositionRangeAsync(params Guid[] positionIds);

        public Task<PositionDto> UpdatePositionAsync(UpdatePositionDto dto);
    }
}
