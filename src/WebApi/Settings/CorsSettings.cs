namespace HomeBudgetManager.Bff.WebUI.WebApi.Settings;

public sealed class CorsSettings
{
    public const string SectionName = "Cors";

    public string[] AllowedOrigins { get; init; } = ["*"];

    public string[] AllowedMethods { get; init; } = ["*"];

    public string[] AllowedHeaders { get; init; } = ["*"];
}
