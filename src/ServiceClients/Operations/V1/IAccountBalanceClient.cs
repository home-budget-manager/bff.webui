namespace HomeBudgetManager.Bff.WebUI.ServiceClients.Operations.V1;

using HomeBudgetManager.Bff.WebUI.ServiceClients.Operations.V1.AccountBalance;

public interface IAccountBalanceClient
{
    Task<BalanceHistoryResult> GetBalanceHistoryAsync(
        Guid accountId,
        BalanceHistoryParameters parameters,
        CancellationToken cancellationToken);
}
