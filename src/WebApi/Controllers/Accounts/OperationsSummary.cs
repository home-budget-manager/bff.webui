namespace HomeBudgetManager.Bff.WebUI.WebApi.Controllers.Accounts;

public class OperationsSummary
{
    public OperationsSummary(
        string currency,
        SummaryItem[] items)
    {
        this.Currency = currency;
        this.Items = items;
    }

    public string Currency { get; }

    public SummaryItem[] Items { get; }
}
