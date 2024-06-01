using Animalsy.BE.Services.VisitAPI.Configuration;
using Microsoft.Extensions.Options;

namespace Animalsy.BE.Services.VisitAPI.Utilities;

internal static partial class AppExtensions
{
    public static void AddHttpClients(this IServiceCollection serviceCollection)
    {
        var provider = serviceCollection.BuildServiceProvider();
        var configuration = provider.GetRequiredService<IOptionsMonitor<ServiceUrlConfiguration>>();

        serviceCollection.AddHttpClient("CustomerApiClient", client =>
            client.BaseAddress = new Uri(configuration.CurrentValue.CustomerApi!));

        serviceCollection.AddHttpClient("ContractorApiClient", client =>
            client.BaseAddress = new Uri(configuration.CurrentValue.ContractorApi!));

        serviceCollection.AddHttpClient("PetApiClient", client => 
            client.BaseAddress = new Uri(configuration.CurrentValue.PetApi!));

        serviceCollection.AddHttpClient("ProductApiClient", client =>
            client.BaseAddress = new Uri(configuration.CurrentValue.ProductApi!));

        serviceCollection.AddHttpClient("VendorApiClient", client => 
            client.BaseAddress = new Uri(configuration.CurrentValue.VendorApi!));
    } 
}