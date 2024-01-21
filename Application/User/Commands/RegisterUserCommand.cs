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

        public RegisterUserCommandHandler(IAuthService authService, IUserRepository userRepository, 
            IPatientRepository patientRepository, IPhysicianRepository physicianRepository, ITimetableRepository timetableRepository) 
        {
            _authService = authService;
            _userRepository = userRepository;
            _patientRepository = patientRepository;
            _physicianRepository = physicianRepository;
            _timetableRepository = timetableRepository;
        }

        public async ValueTask<GetPartialUserDto> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            GetPartialUserDto userDto = null!;
            var response = await _authService.RegisterAsync(command.Dto);

            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                try
                {
                    var userId = Guid.Parse(response.UserSub);
                    userDto = await _userRepository.AddUserAsync(userId, command.Dto);

                    if (userDto.Role == RoleType.Patient)
                    {
                        var patientDto = new AddPatientDto(userId);
                        await _patientRepository.AddPatientDataAsync(patientDto);
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
                    await _authService.DeleteUserAsync(command.Dto.Email);
                }
            }
            
            return userDto;
        }
    }
}
