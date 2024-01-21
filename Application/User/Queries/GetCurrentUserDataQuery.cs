using Application.Common.Interfaces.Services;
using Application.User.DTOs.ResponseDTOs;
using Mediator;

namespace Application.User.Queries
{
    public record GetCurrentUserDataQuery() : IQuery<GetPartialUserDto>;

    public class GetCurrentUserDataHandler : IQueryHandler<GetCurrentUserDataQuery, GetPartialUserDto>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserService _userService;

        public GetCurrentUserDataHandler(ICurrentUserService currentUserService, IUserService userService)
        {
            _currentUserService = currentUserService;
            _userService = userService;
        }

        public async ValueTask<GetPartialUserDto> Handle(GetCurrentUserDataQuery request, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(_currentUserService.UserId);
            var result = await _userService.GetPartialUserAsync(userId);

            return result;
        }
    }

}
