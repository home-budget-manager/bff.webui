namespace HomeBudgetManager.Bff.WebUI.ServiceClients.Operations.V1.AccountBalance;

public class BalanceHistoryResult
{
    public BalanceHistoryResult(
        BalanceHistoryItem[] items,
        string currency,
        decimal startBalance,
        decimal endBalance)
    {
        this.Items = items;
        this.Currency = currency;
        this.StartBalance = startBalance;
        this.EndBalance = endBalance;
    }

    public BalanceHistoryItem[] Items { get; }

    public string Currency { get; }

    public decimal StartBalance { get; }

    public decimal EndBalance { get; }
}
