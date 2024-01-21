using System.Security.Claims;

namespace Application.Common.Interfaces.Services
{
    public interface ICurrentUserService
    {
        public string UserId { get; }
        public string Role { get; }
        public ClaimsPrincipal? UserClaims { get; }
    }
}
