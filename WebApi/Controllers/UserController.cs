using Application.Common.DTOs;
using Application.User.Commands;
using Application.User.DTOs.RequestDTOs;
using Application.User.DTOs.ResponseDTOs;
using Application.User.Queries;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    public class UserController : BaseApiController
    {
        public UserController(IMediator mediator): base(mediator) { }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<GetPartialUserDto>), 200)]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            var result = await _mediator.Send(new RegisterUserCommand(dto));

            var response = result.ToApiResponse();
            return Ok(response);
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<TokenDto>), 200)]
        public async Task<IActionResult> SignIn(AuthCredentialsDto dto)
        {
            var result = await _mediator.Send(new LoginUserCommand(dto));

            var response = result.ToApiResponse();
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<GetPartialUserDto>), 200)]
        public async Task<IActionResult> UpdateUser(UpdateUserDto dto)
        {
            var result = await _mediator.Send(new UpdateUserCommand(dto));

            var response = result.ToApiResponse();
            return Ok(response);
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
        public async Task<IActionResult> ConfirmRegistration(SignUpConfirmationDto dto)
        {
            var result = await _mediator.Send(new ConfirmRegistrationCommand(dto));

            var response = result.ToApiResponse();
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<GetPartialUserDto>), 200)]
        public async Task<IActionResult> GetCurrentUserData()
        {
            var result = await _mediator.Send(new GetCurrentUserDataQuery());

            var response = result.ToApiResponse();
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<GetPartialUserDto>), 200)]
        public async Task<IActionResult> GetPartialUserData([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetUserDataQuery(id));

            var response = result.ToApiResponse();
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<FileDataDto>), 200)]
        public async Task<IActionResult> UploadUserPhoto(IFormFile image)
        {
            await using var fs = image.OpenReadStream();
            var result = await _mediator.Send(new UploadUserPhotoCommand(fs, image.ContentType));

            var response = result.ToApiResponse();
            return Ok(response);
        }
    }
}
