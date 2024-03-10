using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Common.Options;
using Application.User.DTOs.RequestDTOs;
using Application.User.DTOs.ResponseDTOs;
using Application.Common.Enums;
using Microsoft.Extensions.Options;
using System.Net;

namespace Infrastructure.Services
{
    public class CognitoAuthService : IAuthService
    {
        private readonly IAmazonCognitoIdentityProvider _identityProvider;
        private readonly CognitoUserPool _userPool;
        private readonly AwsOptions _awsOptions;

        public CognitoAuthService(IAmazonCognitoIdentityProvider identityProvider, CognitoUserPool userPool, IOptions<AwsOptions> awsOptions)
        {
            _identityProvider = identityProvider;
            _userPool = userPool;
            _awsOptions = awsOptions.Value;
        }

        public async Task<bool> ConfirmSignupAsync(SignUpConfirmationDto dto)
        {
            var signUpRequest = new ConfirmSignUpRequest
            {
                ClientId = _awsOptions.UserPoolClientId,
                ConfirmationCode = dto.Code,
                Username = dto.Email,
            };

            var response = await _identityProvider.ConfirmSignUpAsync(signUpRequest);

            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }

        public async Task<AdminDeleteUserResponse> DeleteUserAsync(string username)
        {
            var deleteRequest = new AdminDeleteUserRequest
            {
                Username = username,
                UserPoolId = _awsOptions.UserPoolId
            };

            var response = await _identityProvider.AdminDeleteUserAsync(deleteRequest);
            return response;
        }

        public async Task<TokenDto> LoginAsync(AuthCredentialsDto credentials)
        {
            var user = new CognitoUser(credentials.Email, _awsOptions.UserPoolClientId, _userPool, _identityProvider);
            var authRequest = new InitiateSrpAuthRequest
            {
                Password = credentials.Password
            };

            var authResponse = await user.StartWithSrpAuthAsync(authRequest);

            var expiresAt = DateTime.Now + TimeSpan.FromSeconds(authResponse.AuthenticationResult.ExpiresIn);
            var idToken = authResponse.AuthenticationResult.IdToken;
            var refreshToken = authResponse.AuthenticationResult.RefreshToken;

            var response = new TokenDto(idToken, refreshToken, expiresAt);

            return response;
        }

        public async Task<SignUpResponse> RegisterAsync(RegisterUserDto registerDto)
        {
            var roleTitle = Enum.GetName(typeof(RoleType), registerDto.Role);
            var request = new SignUpRequest
            {
                Username = registerDto.Email,
                ClientId = _awsOptions.UserPoolClientId,
                Password = registerDto.Password,
                UserAttributes = new List<AttributeType>
                    {
                        new()
                        {
                            Name = "email",
                            Value = registerDto.Email
                        },
                        new()
                        {
                            Name = "custom:role",
                            Value = roleTitle
                        },
                        new()
                        {
                            Name = "phone_number",
                            Value = registerDto.PhoneNumber
                        }
                    },
            };

            var signUpResponse = await _identityProvider.SignUpAsync(request);

            return signUpResponse;
        }
    }
}
