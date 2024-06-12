﻿using Animalsy.BE.Services.AuthAPI.Models.Dto;
using Animalsy.BE.Services.AuthAPI.Services;
using Animalsy.BE.Services.AuthAPI.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Animalsy.BE.Services.AuthAPI.Controllers
{

    [Route("Api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly RegisterUserValidator _registerUserValidator;
        private readonly LoginUserValidator _loginUserValidator;
        private readonly AssignRoleValidator _assignRoleValidator;

        public AuthController(IAuthService authService, RegisterUserValidator registerUserValidator, LoginUserValidator loginUserValidator, AssignRoleValidator assignRoleValidator)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _registerUserValidator = registerUserValidator ?? throw new ArgumentNullException(nameof(registerUserValidator));
            _loginUserValidator = loginUserValidator ?? throw new ArgumentNullException(nameof(loginUserValidator));
            _assignRoleValidator = assignRoleValidator ?? throw new ArgumentNullException(nameof(assignRoleValidator));
        }

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserDto registerUserDto)
        {
            var validationResult = await _registerUserValidator.ValidateAsync(registerUserDto).ConfigureAwait(false);
            if (!validationResult.IsValid) return BadRequest(validationResult);

            var result = await _authService.RegisterAsync(registerUserDto).ConfigureAwait(false);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserDto loginDto)
        {
            var validationResult = await _loginUserValidator.ValidateAsync(loginDto).ConfigureAwait(false);
            if (!validationResult.IsValid) return BadRequest(validationResult);

            var result = await _authService.LoginAsync(loginDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("AssignRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AssignRoleAsync([FromBody] AssignRoleDto assignRoleDto)
        {
            var validationResult = await _assignRoleValidator.ValidateAsync(assignRoleDto).ConfigureAwait(false);
            if (!validationResult.IsValid) return BadRequest(validationResult);

            var result = await _authService.AssignRoleAsync(assignRoleDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
