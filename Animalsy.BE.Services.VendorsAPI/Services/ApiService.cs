namespace Animalsy.BE.Services.VendorAPI.Services;

public class ApiService : IApiService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
    }

    public async Task<HttpResponseMessage> GetAsync(string clientName, string path)
    {
        using var client = _httpClientFactory.CreateClient(clientName);
        return await client.GetAsync(new Uri(client.BaseAddress!, path)).ConfigureAwait(false);
    }
}