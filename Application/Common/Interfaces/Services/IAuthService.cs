using Amazon.CognitoIdentityProvider.Model;
using Application.User.DTOs.RequestDTOs;
using Application.User.DTOs.ResponseDTOs;

namespace Application.Common.Interfaces.Services
{
    public interface IAuthService
    {
        public Task<SignUpResponse> RegisterAsync(RegisterUserDto dto);
        public Task<AdminDeleteUserResponse> DeleteUserAsync(string username);
        public Task<TokenDto> LoginAsync(AuthCredentialsDto credentials);
        public Task<bool> ConfirmSignupAsync(SignUpConfirmationDto dto);
    }
}
