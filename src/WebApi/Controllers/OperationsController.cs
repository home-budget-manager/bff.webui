namespace HomeBudgetManager.Bff.WebUI.WebApi.Controllers;

using HomeBudgetManager.Bff.WebUI.ServiceClients.Operations.V1;
using HomeBudgetManager.Bff.WebUI.ServiceClients.Operations.V1.Operations;
using HomeBudgetManager.Bff.WebUI.WebApi.Controllers.Operations;

using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class OperationsController : ControllerBase
{
    private readonly IOperationsClient operationsClient;

    public OperationsController(IOperationsClient operationsClient)
    {
        this.operationsClient = operationsClient;
    }

    [HttpGet]
    public async Task<IActionResult> SearchOperations(
        [FromQuery] SearchOperationsParameters parameters,
        CancellationToken cancellationToken)
    {
        var searchCriteria = new SearchCriteria(
            parameters.AccountId,
            parameters.From,
            parameters.To,
            parameters.OperationType?.ToString(),
            parameters.Page,
            parameters.PageSize);
        var result = await this.operationsClient.SearchOperations(searchCriteria, cancellationToken);
        if (result is null)
        {
            return this.NoContent();
        }

        var response = new SearchOperationsResult(
            result.Items.Select(o => new SearchOperationsItem(
                o.Id.ToString(),
                o.Date,
                MapOperationType(o.OperationType),
                o.SourceAccountId.ToString(),
                o.TargetAccountId.ToString(),
                o.Title,
                o.Amount,
                o.Currency,
                o.CategoryId,
                o.BudgetId)).ToArray(),
            result.TotalCount);
        return Ok(response);
    }

    private static OperationType MapOperationType(string value)
    {
        return value switch
        {
            "Income" => OperationType.Income,
            "Expense" => OperationType.Expense,
            "Transfer" => OperationType.Transfer,
            _ => throw new ArgumentOutOfRangeException(nameof(value), $"Invalid operation type: {value}"),
        };
    }
}
