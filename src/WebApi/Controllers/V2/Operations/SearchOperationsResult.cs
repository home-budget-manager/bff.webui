namespace HomeBudgetManager.Bff.WebUI.WebApi.Controllers.V2.Operations;

public class SearchOperationsResult
{
    public SearchOperationsResult(
        SearchOperationsItem[] items,
        int totalCount)
    {
        this.Items = items;
        this.TotalCount = totalCount;
    }

    public SearchOperationsItem[] Items { get; }

    public int TotalCount { get; }
}
