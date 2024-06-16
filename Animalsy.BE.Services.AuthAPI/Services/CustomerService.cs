using System.Text;
using Animalsy.BE.Services.AuthAPI.Models.Dto;
using Newtonsoft.Json;

namespace Animalsy.BE.Services.AuthAPI.Services;

public class CustomerService : ICustomerService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CustomerService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
    }

    public async Task<ResponseDto> CreateCustomerAsync(CreateCustomerDto customerDto)
    {

        using var client = _httpClientFactory.CreateClient("CustomerApi");
        using var content = new StringContent(JsonConvert.SerializeObject(customerDto), Encoding.UTF8, "application/json");
        using var response = await client.PostAsync(new Uri("Api/Customer"), content);
        return new ResponseDto
        {
            IsSuccess = response.IsSuccessStatusCode,
            Message = response.IsSuccessStatusCode ? "Customer created successfully" : "Error encountered",
            Result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult()
        };
    }
}