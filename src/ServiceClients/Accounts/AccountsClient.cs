namespace HomeBudgetManager.Bff.WebUI.ServiceClients.Accounts;

using System.Net.Http.Json;

public sealed class AccountsClient : IAccountsClient
{
    private readonly HttpClient client;

    public AccountsClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<IReadOnlyCollection<AccountInfo>> GetUserAccountsAsync(CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/userAccounts");
        var response = await this.client.SendAsync(request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException();
        }

        var result = await response.Content.ReadFromJsonAsync<AccountInfo[]>(cancellationToken: cancellationToken);
        return result ?? [];
    }

    public async Task<AccountDetails> GetAccountDetailsAsync(Guid accountId, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"api/userAccounts/{accountId}");
        var response = await this.client.SendAsync(request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException();
        }

        var result = await response.Content.ReadFromJsonAsync<AccountDetails>(cancellationToken: cancellationToken);
        return result ?? throw new InvalidOperationException();
    }
}
