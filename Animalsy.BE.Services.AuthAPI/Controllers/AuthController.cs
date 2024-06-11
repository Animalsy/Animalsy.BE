using Animalsy.BE.Services.AuthAPI.Models;
using Animalsy.BE.Services.AuthAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Animalsy.BE.Services.AuthAPI.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class AuthController(ICustomerService customerService) : ControllerBase
    {
        public async Task<IActionResult> RegisterAsync(CreateCustomerDto customerDto)
        {

        }
    }
}
