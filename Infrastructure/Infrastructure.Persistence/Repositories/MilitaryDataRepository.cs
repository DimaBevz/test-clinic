using Application.Common.Interfaces.Repositories;
using Application.Military.DTOs.Request;
using Application.Military.DTOs.Response;
using Infrastructure.Persistence.Mappers;

namespace Infrastructure.Persistence.Repositories
{
    internal class MilitaryDataRepository : IMilitaryDataRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MilitaryDataRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetMilitaryDataDto> AddMilitaryDataAsync(Guid userId, AddMilitaryDataDto dto)
        {
            var militaryData = dto.ToEntity();
            militaryData.PatientDataId = userId;

            await _dbContext.MilitaryData.AddAsync(militaryData);
            await _dbContext.SaveChangesAsync();

            var result = militaryData.ToDto();
            return result;
        }
    }
}
