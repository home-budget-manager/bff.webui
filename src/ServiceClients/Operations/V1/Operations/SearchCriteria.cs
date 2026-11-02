namespace HomeBudgetManager.Bff.WebUI.ServiceClients.Operations.V1.Operations;

public class SearchCriteria
{
    public SearchCriteria(
        string? accountId,
        DateTime? from,
        DateTime? to,
        string? operationType,
        int? page,
        int? pageSize)
    {
        this.AccountId = accountId;
        this.From = from;
        this.To = to;
        this.OperationType = operationType;
        this.Page = page;
        this.PageSize = pageSize;
    }

    public string? AccountId { get; }

    public DateTime? From { get; }

    public DateTime? To { get; }

    public string? OperationType { get; }

    public int? Page { get; }

    public int? PageSize { get; }
}
