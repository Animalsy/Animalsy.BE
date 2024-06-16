using System.Text;
using Animalsy.BE.Services.VendorAPI.Models.Dto;
using Newtonsoft.Json;

namespace Animalsy.BE.Services.VendorAPI.Services;

public class AuthService : IAuthService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public AuthService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
    }

    public async Task<ResponseDto> AssignRoleAsync(AssignRoleDto assignRoleDto)
    {
        using var client = _httpClientFactory.CreateClient("AuthApiClient");
        using var content = new StringContent(JsonConvert.SerializeObject(assignRoleDto), Encoding.UTF8, "application/json");
        using var response = await client.PostAsync(new Uri("Api/Auth/AssignRole"), content);
        return new ResponseDto
        {
            IsSuccess = response.IsSuccessStatusCode,
            Result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult()
        };
    }
}