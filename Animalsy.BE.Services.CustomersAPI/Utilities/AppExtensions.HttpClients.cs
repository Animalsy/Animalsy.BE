using Animalsy.BE.Services.CustomerAPI.Configuration;
using Microsoft.Extensions.Options;

namespace Animalsy.BE.Services.CustomerAPI.Utilities;

public static partial class AppExtensions
{
    public static void AddHttpClients(this IServiceCollection serviceCollection)
    {
        var provider = serviceCollection.BuildServiceProvider();
        var configuration = provider.GetRequiredService<IOptionsMonitor<ServiceUrlConfiguration>>();

        serviceCollection.AddHttpClient("PetApiClient", client =>
            client.BaseAddress = new Uri(configuration.CurrentValue.PetApi!));

        serviceCollection.AddHttpClient("VisitApiClient", client =>
            client.BaseAddress = new Uri(configuration.CurrentValue.VisitApi!));
    } 
}