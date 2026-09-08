namespace HomeBudgetManager.Bff.WebUI.WebApi.Controllers.Accounts;

public class OperationsSummary
{
    public OperationsSummary(SummaryItem[] items)
    {
        this.Items = items;
    }

    public SummaryItem[] Items { get; }
}
