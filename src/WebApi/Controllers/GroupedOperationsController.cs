namespace HomeBudgetManager.Bff.WebUI.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;

using HomeBudgetManager.Bff.WebUI.WebApi.Controllers.GroupedOperations;

[Route("api/[controller]")]
[ApiController]
public class GroupedOperationsController
{
    [HttpGet("account/{accountId}/groupType/{groupType}")]
    public IActionResult GetOperationsGrouping(
        string accountId,
        OperationGroupingType groupType,
        [FromQuery]GetOperationsParameters parameters)
    {
        var result = new OperationsInGroup[]
        {
            new(groupType.ToString(), "1", "Food", 5, -150.00m, "USD", parameters.Period),
            new(groupType.ToString(), "2", "Transport", 3, -75.00m, "USD", parameters.Period),
            new(groupType.ToString(), "3", "Entertainment", 2, -50.00m, "USD", parameters.Period),
            new(groupType.ToString(), "4", "Utilities", 1, -100.00m, "USD", parameters.Period),
            new(groupType.ToString(), "5", "Health", 1, -200.00m, "USD", parameters.Period),
        };
        return new OkObjectResult(result);
    }
}
