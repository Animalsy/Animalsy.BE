using System.Collections.Concurrent;
using Newtonsoft.Json;

namespace Animalsy.BE.Services.VisitAPI.Repository.ResponseHandler;

public class ResponseHandler : IResponseHandler
{
    public async Task<T> EvaluateResponse<T>(string objectName, ConcurrentDictionary<string, string> details, Func<Task<HttpResponseMessage>> apiCallFunc)
    {
        using var response = await apiCallFunc.Invoke();
        var content = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }

        details.AddOrUpdate(objectName, response.StatusCode.ToString(), (oldKey, oldValue) => content);
        return default;
    }
}