using Application.Admin.DTOs.Response;
using Application.Common.Interfaces.Services;
using Mediator;

namespace Application.Admin.Queries
{
    public record GetAllUsersQuery() : IQuery<GetAllUsersDto>;

    public class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, GetAllUsersDto>
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUserService;
        private const string Admin = "admin";
        private const string RoleExceptionMessage = "Current user doesn`t belong to admin role";

        public GetAllUsersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async ValueTask<GetAllUsersDto> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            if (_currentUserService.Role.ToLower() != Admin)
            {
                throw new ApplicationException(RoleExceptionMessage);
            }

            var result = await _userService.GetAllUsersAsync(cancellationToken);

            return result;
        }
    }
}
