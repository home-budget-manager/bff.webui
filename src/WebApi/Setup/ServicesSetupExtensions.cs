namespace HomeBudgetManager.Bff.WebUI.WebApi.Setup;

using HomeBudgetManager.Bff.WebUI.ServiceClients;
using HomeBudgetManager.Bff.WebUI.WebApi.Settings;

using Microsoft.Extensions.Options;

public static class ServicesSetupExtensions
{
    public static WebApplicationBuilder AddServices(
        this WebApplicationBuilder builder)
    {
        builder.Services.Configure<ClientsSettings>(builder.Configuration.GetSection("DomainServices"));
        builder.Services.AddServiceClients();
        return builder;
    }
}
