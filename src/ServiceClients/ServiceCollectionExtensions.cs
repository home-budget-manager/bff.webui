namespace HomeBudgetManager.Bff.WebUI.ServiceClients;


using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceClients(
        this IServiceCollection services)
    {
        //services.AddScoped<Accounts.IAccountsClient, Accounts.AccountsClient>();
        services.AddHttpClient<Accounts.IAccountsClient, Accounts.AccountsClient>(
            c => c.Name == "services.accounts");
        services.AddHttpClient<Operations.V1.IOperationsClient, Operations.V1.OperationsClient>(
            c => c.Name == "services.operations");
        return services;
    }

    private static IServiceCollection AddHttpClient<TClient, TImplementation>(
        this IServiceCollection services,
        Func<ClientsSettings.Service, bool> clientSelector)
        where TClient : class
        where TImplementation : class, TClient
    {
        services.AddHttpClient<TClient, TImplementation>((sp, client) =>
        {
            var settings = sp.GetRequiredService<IOptions<ClientsSettings>>().Value;
            var clientSettings = settings.Services.Single(clientSelector);
            client.BaseAddress = new Uri(clientSettings.Url);
        });
        return services;
    }
}
