using Animalsy.BE.Services.AuthAPI.Models.Dto;
using Animalsy.BE.Services.AuthAPI.Services;
using Animalsy.BE.Services.AuthAPI.Validators.Factory;
using Microsoft.AspNetCore.Mvc;

namespace Animalsy.BE.Services.AuthAPI.Controllers
{

    [Route("Api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IValidatorFactory _validatorFactory;

        public AuthController(IAuthService authService, IValidatorFactory validatorFactory)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _validatorFactory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));
        }

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> RegisterAsync([FromBody] RegisterUserDto registerUserDto)
        {
            return HandleRequest(registerUserDto, () => _authService.RegisterAsync(registerUserDto));
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> LoginAsync([FromBody] LoginUserDto loginDto)
        {
            return HandleRequest(loginDto, () => _authService.LoginAsync(loginDto));
        }

        [HttpPost("AssignRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> AssignRoleAsync([FromBody] AssignRoleDto assignRoleDto)
        {
            return HandleRequest(assignRoleDto, () => _authService.AssignRoleAsync(assignRoleDto));
        }


        private async Task<IActionResult> HandleRequest<T>(T dto, Func<Task<ResponseDto>> requestFunc)
        {
            var validator = _validatorFactory.GetValidator<T>();
            if (validator != null)
            {
                var validationResult = await validator.ValidateAsync(dto).ConfigureAwait(false);
                if (!validationResult.IsValid) return BadRequest(validationResult);
            }

            var result = await requestFunc().ConfigureAwait(false);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
