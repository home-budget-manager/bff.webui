namespace HomeBudgetManager.Bff.WebUI.WebApi.Setup;

using HomeBudgetManager.Bff.WebUI.ServiceClients;
using AccountsSdk = HomeBudgetManager.Services.Accounts.Sdk;

using static HomeBudgetManager.Services.Accounts.Sdk.ServiceCollectionExtensions;

public static class ServicesSetupExtensions
{
    public static WebApplicationBuilder AddServices(
        this WebApplicationBuilder builder)
    {
        var clientsSettings = builder.Configuration.GetSection("DomainServices").Get<ClientsSettings>();
        if (clientsSettings is null)
        {
            throw new InvalidOperationException("DomainServices section is missing in configuration.");
        }

        var apiSettingsProvider = new ApiSettingsProvider(clientsSettings);
        builder.Services.RegisterAccountsApi(apiSettingsProvider);
        builder.Services.Configure<ClientsSettings>(builder.Configuration.GetSection("DomainServices"));
        builder.Services.AddServiceClients();
        return builder;
    }

    private static void RegisterAccountsApi(this IServiceCollection services, AccountsSdk.ISettings settings)
    {
        services.AddScoped<Func<AccountsSdk.IRequestBuilder>>(sp => () => new AccountsSdk.RequestBuilder());
        services.AddAccountsSdk(settings);
    }

    private sealed class ApiSettingsProvider(
        ClientsSettings settings) : AccountsSdk.ISettings
    {
        private readonly ClientsSettings settings = settings;

        public string AccountsApiBaseUrl => this.GetUrl("services.accounts");

        private string GetUrl(string serviceName)
        {
            var service = settings.Services.SingleOrDefault(s => s.Name == serviceName);
            if (service is null)
            {
                throw new InvalidOperationException($"Service '{serviceName}' not found in configuration.");
            }

            return service.Url;
        }
    }
}
