using Application.Common.Interfaces.Repositories;
using Application.Position.DTOs.RequestDTOs;
using Application.Position.DTOs.ResponseDTOs;
using Infrastructure.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    internal class PositionRepository : IPositionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PositionRepository(ApplicationDbContext dbContext) 
        { 
            _dbContext = dbContext;
        }

        public async Task<GetPositionsDto> GetPositionsAsync()
        {
            var positionDtos = await _dbContext.Positions
                .Select(p => p.ToDto())
                .ToListAsync();

            var getPositionsDto = new GetPositionsDto(positionDtos);

            return getPositionsDto;
        }

        public async Task<PositionDto> GetPositionByIdAsync(Guid id)
        {
            var position = await _dbContext.Positions.SingleAsync(p => p.Id == id);

            var result = position.ToDto();
            return result;
        }

        public async Task<PositionDto> AddPositionAsync(AddPositionDto dto) 
        {
            var newPosition = dto.ToEntity();

            await _dbContext.Positions.AddAsync(newPosition);
            await _dbContext.SaveChangesAsync();

            var result = newPosition.ToDto();
            return result;
        }

        public async Task<GetPositionsDto> AddPositionRangeAsync(AddPositionListDto dto)
        {
            var positions = dto.Positions.Select(p => p.ToEntity()).ToList();

            await _dbContext.Positions.AddRangeAsync(positions);
            await _dbContext.SaveChangesAsync();

            var positionDtos = positions.Select(p => p.ToDto()).ToList();
            var getPositionsDto = new GetPositionsDto(positionDtos);

            return getPositionsDto;
        }

        public async Task<PositionDto> RemovePositionAsync(Guid positionId)
        {
            var positionToDelete = await _dbContext.Positions.SingleAsync(p => p.Id == positionId);

            _dbContext.Positions.Remove(positionToDelete);
            await _dbContext.SaveChangesAsync();

            var result = positionToDelete.ToDto();
            return result;
        }

        public async Task RemovePositionRangeAsync(params Guid[] positionIds)
        {
            var positionsToDelete = await _dbContext.Positions
                .Where(p => positionIds.Contains(p.Id))
                .ToListAsync();

            _dbContext.Positions.RemoveRange(positionsToDelete);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<PositionDto> UpdatePositionAsync(UpdatePositionDto dto)
        {
            var positionToUpdate = await _dbContext.Positions.SingleAsync(p => p.Id == dto.Id);
            dto.ToCurrentEntity(positionToUpdate);

            _dbContext.Positions.Update(positionToUpdate);
            await _dbContext.SaveChangesAsync();

            var result = positionToUpdate.ToDto();
            return result;
        }
    }
}
