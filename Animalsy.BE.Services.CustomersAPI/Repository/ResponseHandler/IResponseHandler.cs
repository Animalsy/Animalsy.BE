using System.Collections.Concurrent;

namespace Animalsy.BE.Services.CustomerAPI.Repository.ResponseHandler;

public interface IResponseHandler
{
    Task<T> EvaluateResponse<T>(string objectName, ConcurrentDictionary<string, string> details, Func<Task<HttpResponseMessage>> apiCallFunc);
}