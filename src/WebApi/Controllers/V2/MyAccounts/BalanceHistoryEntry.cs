namespace HomeBudgetManager.Bff.WebUI.WebApi.Controllers.V2.MyAccounts;

public class BalanceHistoryEntry
{
    public BalanceHistoryEntry(
        DateTime date,
        decimal balance)
    {
        this.Date = date;
        this.Balance = balance;
    }

    public DateTime Date { get; }

    public decimal Balance { get; }
}
