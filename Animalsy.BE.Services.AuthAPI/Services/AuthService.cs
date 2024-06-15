using Animalsy.BE.Services.AuthAPI.Data;
using Animalsy.BE.Services.AuthAPI.Models;
using Animalsy.BE.Services.AuthAPI.Models.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Animalsy.BE.Services.AuthAPI.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ICustomerService _customerService;
    private readonly IMapper _mapper;
    private readonly IJwtTokenService _tokenService;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        AppDbContext dbContext,
        UserManager<ApplicationUser> userManager,
        ICustomerService customerService,
        IMapper mapper,
        IJwtTokenService tokenGenerator, ILogger<AuthService> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _tokenService = tokenGenerator ?? throw new ArgumentNullException(nameof(tokenGenerator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<ResponseDto> RegisterAsync(RegisterUserDto registerUserDto)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync().ConfigureAwait(false);

        try
        {
            var user = _mapper.Map<ApplicationUser>(registerUserDto);
            var userManagerCreationResult = await _userManager.CreateAsync(user, registerUserDto.Password).ConfigureAwait(false);

            if (!userManagerCreationResult.Succeeded)
            {
                await transaction.RollbackAsync().ConfigureAwait(false);
                return new ResponseDto
                {
                    IsSuccess = false,
                    Result = userManagerCreationResult
                };
            }

            var createdUser = await _dbContext.Users.SingleAsync(u =>
                u.Email.Equals(registerUserDto.EmailAddress, StringComparison.OrdinalIgnoreCase)).ConfigureAwait(false);

            var createCustomerDto = _mapper.Map<CreateCustomerDto>(registerUserDto);
            createCustomerDto.UserId = createdUser.Id;

            var creationResult = await _customerService.CreateCustomerAsync(createCustomerDto).ConfigureAwait(false);
            await transaction.CommitAsync().ConfigureAwait(false);

            return new ResponseDto
            {
                IsSuccess = true,
                Result = new RegisterUserResponseDto
                {
                    UserId = createdUser.Id,
                    CustomerId = JsonConvert.DeserializeObject<Guid>(creationResult.Result.ToString()!)
                },
                Message = "User registered successfully"
            };
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync().ConfigureAwait(false);
            _logger.LogError(ex, message: "Error occurred during user registration");
            return new ResponseDto
            {
                IsSuccess = false,
                Result = ex,
                Message = "Error encountered"
            };
        }
    }

    public async Task<ResponseDto> LoginAsync(LoginUserDto loginUserDto)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u =>
            u.Email.Equals(loginUserDto.Email, StringComparison.OrdinalIgnoreCase)).ConfigureAwait(false);

        if (user == null || !await _userManager.CheckPasswordAsync(user, loginUserDto.Password).ConfigureAwait(false))
        {
            throw new UnauthorizedAccessException("Invalid username or password");
        }

        var userRoles = _userManager.GetRolesAsync(user).GetAwaiter().GetResult();

        return new ResponseDto
        {
            IsSuccess = true,
            Result = new LoginUserResponseDto
            {
                UserId = user.Id,
                Token = _tokenService.GenerateToken(user, userRoles)
            },
            Message = "User logged in"
        };
    }

    public async Task<ResponseDto> AssignRoleAsync(AssignRoleDto assignRoleDto)
    {
        var user = _dbContext.Users.FirstOrDefault(user => user.Email.Equals(assignRoleDto.Email, StringComparison.OrdinalIgnoreCase));
        if (user == null)
            return new ResponseDto
            {
                IsSuccess = false,
                Message = "User not found"
            };

        var result = await _userManager.AddToRoleAsync(user, assignRoleDto.RoleName);
        return result.Succeeded
            ? new ResponseDto
            {
                IsSuccess = true,
                Result = result,
                Message = "User assigned to role successfully"
            }
            : new ResponseDto
            {
                IsSuccess = false,
                Result = result
            };
    }
}