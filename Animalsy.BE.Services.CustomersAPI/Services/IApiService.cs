namespace Animalsy.BE.Services.CustomerAPI.Services;

public interface IApiService
{
    Task<HttpResponseMessage> GetAsync(string clientName, string path);
}