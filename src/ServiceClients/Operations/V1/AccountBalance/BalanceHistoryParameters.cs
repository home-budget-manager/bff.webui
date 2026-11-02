namespace HomeBudgetManager.Bff.WebUI.ServiceClients.Operations.V1.AccountBalance;

public class BalanceHistoryParameters
{
    public BalanceHistoryParameters(DateTime? fromDate, DateTime? toDate)
    {
        this.FromDate = fromDate;
        this.ToDate = toDate;
    }

    public DateTime? FromDate { get; }

    public DateTime? ToDate { get; }
}
