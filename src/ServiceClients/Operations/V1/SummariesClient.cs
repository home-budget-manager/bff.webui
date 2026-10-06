namespace HomeBudgetManager.Bff.WebUI.ServiceClients.Operations.V1;

using System.Net.Http.Json;

using HomeBudgetManager.Bff.WebUI.ServiceClients.Operations.V1.Summaries;

public sealed class SummariesClient : ISummariesClient
{
    private readonly HttpClient client;

    public SummariesClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<GetSummariesResult?> GetSummariesAsync(Guid accountId, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"api/v1/summaries/{accountId}");
        var response = await this.client.SendAsync(request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException();
        }

        var result = await response.Content.ReadFromJsonAsync<GetSummariesResult>(cancellationToken);
        return result;
    }
}
