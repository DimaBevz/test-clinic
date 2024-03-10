using Application.Military.DTOs.Request;
using Application.Military.DTOs.Response;

namespace Application.Common.Interfaces.Repositories
{
    public interface IMilitaryDataRepository
    {
        public Task<GetMilitaryDataDto> AddMilitaryDataAsync(Guid userId, AddMilitaryDataDto dto);
    }
}
