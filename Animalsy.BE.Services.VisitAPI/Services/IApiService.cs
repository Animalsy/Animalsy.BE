namespace Animalsy.BE.Services.VisitAPI.Services;

public interface IApiService
{
    Task<T> GetAsync<T>(string clientName, string path);
}