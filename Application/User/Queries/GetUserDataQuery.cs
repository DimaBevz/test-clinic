using Application.Common.Interfaces.Services;
using Application.User.DTOs.ResponseDTOs;
using Mediator;

namespace Application.User.Queries
{
    public record GetUserDataQuery(Guid UserId) : IQuery<GetPartialUserDto>;

    public class GetUserDataHandler : IQueryHandler<GetUserDataQuery, GetPartialUserDto>
    {
        private readonly IUserService _userService;

        public GetUserDataHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async ValueTask<GetPartialUserDto> Handle(GetUserDataQuery request, CancellationToken cancellationToken)
        {
            var result = await _userService.GetPartialUserAsync(request.UserId);
            return result;
        }
    }
}
