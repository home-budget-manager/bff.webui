namespace HomeBudgetManager.Bff.WebUI.ServiceClients;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceClients(
        this IServiceCollection services)
    {
        services.AddScoped<Accounts.IAccountsClient, Accounts.AccountsClient>();
        services.AddHttpClient<Accounts.IAccountsClient, Accounts.AccountsClient>((sp, client) =>
        {
            var settings = sp.GetRequiredService<IOptions<ClientsSettings>>().Value;
            var clientSettings = settings.Services.Single(c => c.Name == "services.accounts");
            client.BaseAddress = new Uri(clientSettings.Url);
        });
        return services;
    }
}
