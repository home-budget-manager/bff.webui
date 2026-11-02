namespace HomeBudgetManager.Bff.WebUI.WebApi.Setup;

using HomeBudgetManager.Bff.WebUI.WebApi.Settings;

using Microsoft.AspNetCore.Cors.Infrastructure;

public static class CorsSetupExtensions
{
    public static WebApplicationBuilder AddCors(
        this WebApplicationBuilder builder)
    {
        var corsSettings = builder.ReadCorsConfiguration();
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policyBuilder =>
            {
                policyBuilder.Configure(corsSettings.AllowedOrigins, static builder => builder.AllowAnyOrigin(), static (builder, values) => builder.WithOrigins(values));
                policyBuilder.Configure(corsSettings.AllowedMethods, static builder => builder.AllowAnyMethod(), static (builder, values) => builder.WithMethods(values));
                policyBuilder.Configure(corsSettings.AllowedHeaders, static builder => builder.AllowAnyHeader(), static (builder, values) => builder.WithHeaders(values));
            });
        });
        return builder;
    }

    private static void Configure(
        this CorsPolicyBuilder policyBuilder,
        string[] values,
        Action<CorsPolicyBuilder> allowAny,
        Action<CorsPolicyBuilder, string[]> allowConfigured)
    {
        if (values.Length == 0 || values.Contains("*", StringComparer.Ordinal))
        {
            allowAny(policyBuilder);
            return;
        }

        allowConfigured(policyBuilder, values);
    }

    private static CorsSettings ReadCorsConfiguration(
        this WebApplicationBuilder builder)
    {
        return builder.Configuration.GetSection(CorsSettings.SectionName).Get<CorsSettings>() ?? new();
    }
}
