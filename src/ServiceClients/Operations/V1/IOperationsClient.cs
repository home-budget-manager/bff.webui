namespace HomeBudgetManager.Bff.WebUI.ServiceClients.Operations.V1;

using HomeBudgetManager.Bff.WebUI.ServiceClients.Operations.V1.Operations;

public interface IOperationsClient
{
    Task<SearchResults?> SearchOperations(
        SearchCriteria searchCriteria,
        CancellationToken cancellationToken);
}
