﻿using Newtonsoft.Json;

namespace Animalsy.BE.Services.VisitAPI.Services
{
    public class ApiService(IHttpClientFactory httpClientFactory) : IApiService
    {
        public async Task<T> GetAsync<T>(string clientName, string path, Guid id)
        {
            using var client = httpClientFactory.CreateClient(clientName);
            using var response = await client.GetAsync(new Uri($"{path}/{id}")).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
