using Animalsy.BE.Services.AuthAPI.Models.Dto;
using Animalsy.BE.Services.AuthAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Animalsy.BE.Services.AuthAPI.Controllers
{

    [Route("Api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterAsync(RegisterUserDto customerDto)
        {
            var result = await _authService.RegisterAsync(customerDto).ConfigureAwait(false);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoginAsync(LoginUserDto loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
