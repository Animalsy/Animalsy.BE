namespace Animalsy.BE.Services.VisitAPI.Services;

public interface IApiService
{
    Task<HttpResponseMessage> GetAsync(string clientName, string path);
}