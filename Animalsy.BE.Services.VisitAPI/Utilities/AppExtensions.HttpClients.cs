using Animalsy.BE.Services.VisitAPI.Configuration;
using Microsoft.Extensions.Options;

namespace Animalsy.BE.Services.VisitAPI.Utilities;

internal static partial class AppExtensions
{
    public static void AddHttpClients(this IServiceCollection serviceCollection, IOptionsMonitor<ServiceUrlConfiguration> configuration)
    {
        serviceCollection.AddHttpClient("CustomerApiClient", client =>
            client.BaseAddress = new Uri(configuration.CurrentValue.CustomerApi!));

        serviceCollection.AddHttpClient("PetApiClient", client => 
            client.BaseAddress = new Uri(configuration.CurrentValue.PetApi!));

        serviceCollection.AddHttpClient("VendorApiClient", client => 
            client.BaseAddress = new Uri(configuration.CurrentValue.VendorApi!));
    } 
}