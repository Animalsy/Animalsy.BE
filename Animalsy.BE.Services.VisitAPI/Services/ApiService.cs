namespace Animalsy.BE.Services.VisitAPI.Services
{
    public class ApiService : IApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<HttpResponseMessage> GetAsync(string clientName, string path)
        {
            using var client = _httpClientFactory.CreateClient(clientName);
            var token = _httpContextAccessor?.HttpContext?.Request.Headers.Authorization.FirstOrDefault()?.Replace("Bearer ", "");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            return await client.GetAsync(new Uri(client.BaseAddress!, path)).ConfigureAwait(false);
        }
    }
}
