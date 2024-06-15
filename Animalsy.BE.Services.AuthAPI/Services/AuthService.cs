using Animalsy.BE.Services.AuthAPI.Data;
using Animalsy.BE.Services.AuthAPI.Models;
using Animalsy.BE.Services.AuthAPI.Models.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Animalsy.BE.Services.AuthAPI.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ICustomerService _customerService;
    private readonly IMapper _mapper;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public AuthService(AppDbContext dbContext, UserManager<ApplicationUser> userManager, ICustomerService customerService,
        IMapper mapper, IJwtTokenGenerator tokenGenerator)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _tokenGenerator = tokenGenerator ?? throw new ArgumentNullException(nameof(tokenGenerator));
    }

    public async Task<ResponseDto> RegisterAsync(RegisterUserDto registerUserDto)
    {
        try
        {
            var result = await _userManager.CreateAsync(
                _mapper.Map<ApplicationUser>(registerUserDto), registerUserDto.Password).ConfigureAwait(false);

            if (!result.Succeeded) return new ResponseDto
            {
                IsSuccess = false,
                Result = result
            };

            var user = await _dbContext.Users.SingleAsync(user => 
                    user.Email.Equals(registerUserDto.EmailAddress, StringComparison.OrdinalIgnoreCase))
                .ConfigureAwait(false);

            var createCustomerDto = _mapper.Map<CreateCustomerDto>(registerUserDto);
                createCustomerDto.UserId = user.Id;

            return await _customerService.CreateCustomerAsync(createCustomerDto)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            return new ResponseDto
            {
                IsSuccess = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ResponseDto> LoginAsync(LoginUserDto loginUserDto)
    {
        try
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(user =>
                user.Email.Equals(loginUserDto.Email, StringComparison.OrdinalIgnoreCase)).ConfigureAwait(false);

            return user != null && await _userManager.CheckPasswordAsync(user, loginUserDto.Password)
                ? new ResponseDto
                {
                    IsSuccess = true,
                    Result = new LoginUserResponseDto
                    {
                        UserId = user.Id,
                        Token = _tokenGenerator.GenerateToken(user)
                    },
                    Message = "User logged in"
                }
                : new ResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid username or password"
                };
        }
        catch (Exception ex)
        {
            return new ResponseDto
            {
                IsSuccess = false,
                Message = ex.Message
            };
        }
        
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