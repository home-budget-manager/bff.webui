namespace HomeBudgetManager.Bff.WebUI.WebApi.Controllers.Accounts;

public class AccountBalanceHistory
{
    public AccountBalanceHistory(
        string accountId,
        string currency,
        BalanceHistoryEntry[] balanceHistory)
    {
        this.AccountId = accountId;
        this.Currency = currency;
        this.BalanceHistory = balanceHistory;
    }

    public string AccountId { get; }

    public string Currency { get; }

    public BalanceHistoryEntry[] BalanceHistory { get; }
}
