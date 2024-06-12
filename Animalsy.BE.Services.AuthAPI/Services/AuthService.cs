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
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ICustomerService _customerService;
    private readonly IMapper _mapper;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public AuthService(AppDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
        ICustomerService customerService, IMapper mapper, IJwtTokenGenerator tokenGenerator)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
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

            return result.Succeeded
                ? await _customerService.CreateCustomerAsync(_mapper.Map<CreateCustomerDto>(registerUserDto))
                    .ConfigureAwait(false)
                : new ResponseDto
                {
                    IsSuccess = false,
                    Result = result
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

    public async Task<ResponseDto> LoginAsync(LoginUserDto loginUserDto)
    {
        try
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(user =>
                user.UserName.Equals(loginUserDto.Email, StringComparison.OrdinalIgnoreCase)).ConfigureAwait(false);

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

    public Task<ResponseDto> AssignRoleAsync(string email, string roleName)
    {
        throw new NotImplementedException();
    }
}