using Application.Common.Interfaces.Repositories;
using Application.Patient.DTOs.RequestDTOs;
using Application.Patient.DTOs.ResponseDTOs;
using Infrastructure.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    internal class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PatientRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetPatientDataDto> AddPatientDataAsync(AddPatientDto patientDto)
        {
            var patientData = patientDto.ToEntity();

            await _dbContext.PatientData.AddAsync(patientData);
            await _dbContext.SaveChangesAsync();

            var dto = patientData.ToDto();
            return dto;
        }

        public async Task<GetPatientDataDto> GetPatientDataAsync(Guid id)
        {
            var patientData = await _dbContext.PatientData.SingleAsync(x => x.UserId == id);

            var dto = patientData.ToDto();
            return dto;
        }

        public async Task<GetPatientDataDto> UpdatePatientAsync(UpdatePatientDto updatePatientDto)
        {
            var currentEntityToUpdate = await _dbContext.PatientData.SingleAsync(p => p.UserId == updatePatientDto.Id);
            updatePatientDto.ToCurrentEntity(currentEntityToUpdate);

            _dbContext.PatientData.Update(currentEntityToUpdate);
            await _dbContext.SaveChangesAsync();

            var dto = currentEntityToUpdate.ToDto();
            return dto;
        }
    }
}
