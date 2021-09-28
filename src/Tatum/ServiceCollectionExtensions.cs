using Microsoft.Extensions.DependencyInjection;
using TatumPlatform.Clients;

namespace TatumPlatform
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTatum(this IServiceCollection services, string apiBaseUrl, string xApiKey)
        {
            return services.AddScoped(provider => BitcoinCashClient.Create(apiBaseUrl, xApiKey))
                            .AddScoped(provider => BitcoinClient.Create(apiBaseUrl, xApiKey))
                            .AddScoped(provider => EthereumClient.Create(apiBaseUrl, xApiKey))
                            .AddScoped(provider => LitecoinClient.Create(apiBaseUrl, xApiKey))
                            .AddScoped(provider => TatumClient.Create(apiBaseUrl, xApiKey))
                            .AddScoped(provider => VeChainClient.Create(apiBaseUrl, xApiKey))
                            .AddScoped(provider => XlmClient.Create(apiBaseUrl, xApiKey))
                            .AddScoped(provider => XrpClient.Create(apiBaseUrl, xApiKey))
                            .AddScoped(provider => TronClient.Create(apiBaseUrl, xApiKey))
                            .AddScoped(provider => AdaClient.Create(apiBaseUrl, xApiKey))
                            .AddScoped(provider => DogecoinClient.Create(apiBaseUrl, xApiKey))
                            .AddScoped(provider => BscClient.Create(apiBaseUrl, xApiKey));
        }
    }
}
