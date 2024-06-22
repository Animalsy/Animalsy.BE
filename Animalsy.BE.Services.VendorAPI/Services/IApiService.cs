namespace Animalsy.BE.Services.VendorAPI.Services;

public interface IApiService
{
    Task<HttpResponseMessage> GetAsync(string clientName, string path);
}