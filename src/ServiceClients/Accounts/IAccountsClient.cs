namespace HomeBudgetManager.Bff.WebUI.ServiceClients.Accounts;

public interface IAccountsClient
{
    Task<IReadOnlyCollection<AccountInfo>> GetUserAccountsAsync(
        CancellationToken cancellationToken);
}
