using System.Text;
using Animalsy.BE.Services.AuthAPI.Models;
using Newtonsoft.Json;

namespace Animalsy.BE.Services.AuthAPI.Services;

public class CustomerService(IHttpClientFactory httpClientFactory) : ICustomerService
{
    public async Task<ResponseDto> CreateCustomerAsync(CreateCustomerDto customerDto)
    {
        try
        {
            using var client = httpClientFactory.CreateClient("CustomerApi");
            using var content = new StringContent(JsonConvert.SerializeObject(customerDto), Encoding.UTF8, "application/json");
            using var response = await client.PostAsync(new Uri("Api/Customer"), content);
            return new ResponseDto
            {
                IsSuccess = response.IsSuccessStatusCode,
                Message = response.IsSuccessStatusCode ? "Customer created successfully" : "Error encountered",
                Result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult()
            };
        }
        catch (Exception ex)
        {
            return new ResponseDto
            {
                IsSuccess = false,
                Message = ex.Message,
            };
        }
        
    }
}