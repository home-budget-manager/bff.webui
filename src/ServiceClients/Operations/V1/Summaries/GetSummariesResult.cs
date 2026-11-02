namespace HomeBudgetManager.Bff.WebUI.ServiceClients.Operations.V1.Summaries;

public class GetSummariesResult
{
    public GetSummariesResult(
        string currency,
        SummaryItem[] summaries)
    {
        this.Currency = currency;
        this.Summaries = summaries;
    }

    public string Currency { get; }

    public SummaryItem[] Summaries { get; }
}
