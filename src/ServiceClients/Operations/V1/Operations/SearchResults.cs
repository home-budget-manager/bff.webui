namespace HomeBudgetManager.Bff.WebUI.ServiceClients.Operations.V1.Operations;

using System.Collections.ObjectModel;

public class SearchResults
{
    public SearchResults(
        SearchResultItem[] items,
        int totalCount)
    {
        this.Items = items;
        this.TotalCount = totalCount;
    }

    public SearchResultItem[] Items { get; }

    public int TotalCount { get; }
}
