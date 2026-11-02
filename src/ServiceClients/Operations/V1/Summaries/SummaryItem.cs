namespace HomeBudgetManager.Bff.WebUI.ServiceClients.Operations.V1.Summaries;

public class SummaryItem
{
    public SummaryItem(
        string itemType,
        int count,
        decimal amount)
    {
        this.ItemType = itemType;
        this.Count = count;
        this.Amount = amount;
    }

    public string ItemType { get; }

    public int Count { get; }

    public decimal Amount { get; }
}
