using Application.Admin.DTOs.Response;
using Application.Common.Interfaces.Services;
using Mediator;

namespace Application.Admin.Queries
{
    public record GetUnapprovedPhysiciansQuery() : IQuery<GetUnapprovedPhysiciansDto>;

    public class GetUnapprovedPhysiciansQueryHandler : IQueryHandler<GetUnapprovedPhysiciansQuery, GetUnapprovedPhysiciansDto>
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUserService;
        private const string Admin = "admin";
        private const string RoleExceptionMessage = "Current user doesn`t belong to admin role";

        public GetUnapprovedPhysiciansQueryHandler(IUserService userService, ICurrentUserService currentUserService)
        {
            _userService = userService;
            _currentUserService = currentUserService;
        }

        public async ValueTask<GetUnapprovedPhysiciansDto> Handle(GetUnapprovedPhysiciansQuery query, CancellationToken cancellationToken)
        {
            if (_currentUserService.Role.ToLower() != Admin)
            {
                throw new ApplicationException(RoleExceptionMessage);
            }

            var result = await _userService.GetUnapprovedPhysiciansAsync(cancellationToken);

            return result;
        }
    }
}
