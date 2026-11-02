namespace HomeBudgetManager.Bff.WebUI.ServiceClients.Operations.V1;

using System.Net.Http.Json;

using HomeBudgetManager.Bff.WebUI.ServiceClients.Operations.V1.Operations;

public sealed class OperationsClient : IOperationsClient
{
    private readonly HttpClient client;

    public OperationsClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<SearchResults?> SearchOperations(SearchCriteria searchCriteria, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/v1/operations");
        var response = await this.client.SendAsync(request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException();
        }

        var result = await response.Content.ReadFromJsonAsync<SearchResults>(cancellationToken: cancellationToken);
        return result;
    }
}
