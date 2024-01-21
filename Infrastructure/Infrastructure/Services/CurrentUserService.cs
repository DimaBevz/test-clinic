using System.Security.Claims;
using Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Http;


namespace Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId => _httpContextAccessor.HttpContext!.User.FindFirst("cognito:username").Value;
        public string Role => _httpContextAccessor.HttpContext!.User.FindFirst("custom:role").Value;
        public ClaimsPrincipal? UserClaims => _httpContextAccessor.HttpContext?.User;
    }
}
