using Application.Common.Enums;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Patient.DTOs.RequestDTOs;
using Application.Physician.DTOs.RequestDTOs;
using Application.User.DTOs.RequestDTOs;
using Application.User.DTOs.ResponseDTOs;
using Mediator;
using System.Net;

namespace Application.User.Commands
{
    public record RegisterUserCommand(RegisterUserDto Dto) : ICommand<GetPartialUserDto>;

    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, GetPartialUserDto>
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IPhysicianRepository _physicianRepository;
        private readonly ITimetableRepository _timetableRepository;
        private readonly IMilitaryDataRepository _militaryDataRepository;

        public RegisterUserCommandHandler(IAuthService authService, IUserRepository userRepository, 
            IPatientRepository patientRepository, IPhysicianRepository physicianRepository, ITimetableRepository timetableRepository,
            IMilitaryDataRepository militaryDataRepository) 
        {
            _authService = authService;
            _userRepository = userRepository;
            _patientRepository = patientRepository;
            _physicianRepository = physicianRepository;
            _timetableRepository = timetableRepository;
            _militaryDataRepository = militaryDataRepository;
        }

        public async ValueTask<GetPartialUserDto> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            GetPartialUserDto userDto = null!;
            var response = await _authService.RegisterAsync(command.Dto);

            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                var userId = Guid.Parse(response.UserSub);

                try
                {
                    userDto = await _userRepository.AddUserAsync(userId, command.Dto);

                    if (userDto.Role == RoleType.Patient)
                    {
                        var patientDto = new AddPatientDto(userId);
                        await _patientRepository.AddPatientDataAsync(patientDto);

                        if (command.Dto.MilitaryData is not null)
                        {
                            await _militaryDataRepository.AddMilitaryDataAsync(userId, command.Dto.MilitaryData);
                        }
                    }
                    else if (userDto.Role == RoleType.Physician)
                    {
                        var patientDto = new AddPhysicianDto(userId);
                        await _physicianRepository.AddPhysicianDataAsync(patientDto);
                        await _timetableRepository.CreateDefaultTemplateAsync(userId, cancellationToken);
                    }
                }
                catch
                {
                    await _userRepository.RemoveUserAsync(userId);
                    await _authService.DeleteUserAsync(command.Dto.Email);
                }
            }
            
            return userDto;
        }
    }
}
