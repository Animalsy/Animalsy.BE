namespace Animalsy.BE.Services.VendorAPI.Services
{
    public interface IApiService
    {
        Task<T> GetAsync<T>(string clientName, string path, Guid id);
    }
}
