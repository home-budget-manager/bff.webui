namespace HomeBudgetManager.Bff.WebUI.WebApi.Controllers.MyAccounts;

public class AccountBalanceHistory
{
    public AccountBalanceHistory(
        Guid accountId,
        string currency,
        BalanceHistoryEntry[] balanceHistory)
    {
        this.AccountId = accountId;
        this.Currency = currency;
        this.BalanceHistory = balanceHistory;
    }

    public Guid AccountId { get; }

    public string Currency { get; }

    public BalanceHistoryEntry[] BalanceHistory { get; }
}
