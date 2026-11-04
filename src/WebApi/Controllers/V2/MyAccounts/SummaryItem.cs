namespace HomeBudgetManager.Bff.WebUI.WebApi.Controllers.V2.MyAccounts;

public class SummaryItem
{
    public SummaryItem(
        SummaryItemType itemType,
        int count,
        decimal amount)
    {
        this.ItemType = itemType;
        this.Count = count;
        this.Amount = amount;
    }

    public SummaryItemType ItemType { get; }

    public int Count { get; }

    public decimal Amount { get; }
}
