namespace HomeBudgetManager.Bff.WebUI.ServiceClients;

public sealed class ClientsSettings
{
    public List<Service> Services { get; set; } = [];

    public sealed class Service
    {
        public string Name { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;
    }
}
