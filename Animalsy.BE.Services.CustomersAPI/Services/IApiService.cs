namespace Animalsy.BE.Services.CustomerAPI.Services
{
    public interface IApiService
    {
        Task<T> GetAsync<T>(string clientName, string path);
    }
}
