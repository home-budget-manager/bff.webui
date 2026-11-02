namespace HomeBudgetManager.Bff.WebUI.ServiceClients.Operations.V1;

using HomeBudgetManager.Bff.WebUI.ServiceClients.Operations.V1.Summaries;

public interface ISummariesClient
{
    Task<GetSummariesResult?> GetSummariesAsync(
        Guid accountId,
        CancellationToken cancellationToken);
}
