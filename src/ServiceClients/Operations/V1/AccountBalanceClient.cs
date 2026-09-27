namespace HomeBudgetManager.Bff.WebUI.ServiceClients.Operations.V1;

using System.Net.Http.Json;

using HomeBudgetManager.Bff.WebUI.ServiceClients.Operations.V1.AccountBalance;

using Microsoft.AspNetCore.WebUtilities;

public sealed class AccountBalanceClient : IAccountBalanceClient
{
    private readonly HttpClient client;

    public AccountBalanceClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<BalanceHistoryResult> GetBalanceHistoryAsync(Guid accountId, BalanceHistoryParameters parameters, CancellationToken cancellationToken)
    {
        var path = $"api/v1/accountBalance/{accountId}";
        path = QueryHelpers.AddQueryString(path, new Dictionary<string, string?>
        {
            { "fromDate", parameters.FromDate?.ToString("yyyy-MM-dd") },
            { "toDate", parameters.ToDate?.ToString("yyyy-MM-dd") }
        });
        var request = new HttpRequestMessage(HttpMethod.Get, path);
        var response = await this.client.SendAsync(request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException();
        }

        var result = await response.Content.ReadFromJsonAsync<BalanceHistoryResult>(cancellationToken: cancellationToken);
        if (result is null)
        {
            return new BalanceHistoryResult(Array.Empty<BalanceHistoryItem>(), string.Empty, 0m, 0m);
        }

        return result;
    }
}
