using Animalsy.BE.Services.VendorAPI.Configuration;
using Microsoft.Extensions.Options;

namespace Animalsy.BE.Services.VendorAPI.Utilities;

public static partial class AppExtensions
{
    public static void AddHttpClients(this IServiceCollection serviceCollection)
    {
        var provider = serviceCollection.BuildServiceProvider();
        var configuration = provider.GetRequiredService<IOptionsMonitor<ServiceUrlConfiguration>>();

        serviceCollection.AddHttpClient("CustomerApiClient", client =>
            client.BaseAddress = new Uri(configuration.CurrentValue.CustomerApi!));

        serviceCollection.AddHttpClient("VisitApiClient", client =>
            client.BaseAddress = new Uri(configuration.CurrentValue.VisitApi!));
    } 
}