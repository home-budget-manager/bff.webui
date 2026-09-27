namespace HomeBudgetManager.Bff.WebUI.ServiceClients.Operations.V1.AccountBalance;

public class BalanceHistoryItem
{
    public BalanceHistoryItem(
        DateTime date,
        decimal balance)
    {
        this.Date = date;
        this.Balance = balance;
    }

    public DateTime Date { get; }

    public decimal Balance { get; }
}
