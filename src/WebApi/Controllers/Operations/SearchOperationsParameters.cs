namespace HomeBudgetManager.Bff.WebUI.WebApi.Controllers.Operations;

public class SearchOperationsParameters
{
    public string? AccountId { get; set; }

    public DateTime? From { get; set; }

    public DateTime? To { get; set; }

    public OperationType? OperationType { get; set; }

    public int? Page { get; set; }

    public int? PageSize { get; set; }
}
