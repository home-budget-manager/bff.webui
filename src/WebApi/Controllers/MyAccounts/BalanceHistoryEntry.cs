namespace HomeBudgetManager.Bff.WebUI.WebApi.Controllers.MyAccounts;

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
