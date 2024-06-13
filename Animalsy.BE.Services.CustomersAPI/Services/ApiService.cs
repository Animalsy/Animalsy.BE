using Newtonsoft.Json;

namespace Animalsy.BE.Services.CustomerAPI.Services;

public class ApiService : IApiService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
    }

    public async Task<T> GetAsync<T>(string clientName, string path)
    {
        using var client = _httpClientFactory.CreateClient(clientName);
        using var response = await client.GetAsync(new Uri(path)).ConfigureAwait(false);
        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        return JsonConvert.DeserializeObject<T>(content);
    }
}