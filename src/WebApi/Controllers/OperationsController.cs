namespace HomeBudgetManager.Bff.WebUI.WebApi.Controllers;

using HomeBudgetManager.Bff.WebUI.WebApi.Controllers.Operations;

using Microsoft.AspNetCore.Mvc;

using static System.Runtime.InteropServices.JavaScript.JSType;

[Route("api/[controller]")]
[ApiController]
public class OperationsController : ControllerBase
{
    [HttpGet]
    public IActionResult SearchOperations([FromQuery] SearchOperationsParameters parameters)
    {

        var result = new SearchOperationsResult(
            new[]
            {
                new SearchOperationsItem(
                    "1",
                    DateTime.Now,
                    OperationType.Expense,
                    "1",
                    "2",
                    "Grocery Shopping",
                    -50,
                    "USD",
                    "1",
                    "1"
                ),
                new SearchOperationsItem(
                    "2",
                    DateTime.Now.AddHours(-5).AddMinutes(-43),
                    OperationType.Transfer,
                    "2",
                    "3",
                    "Transfer to savings account",
                    -520,
                    "USD",
                    "2",
                    "2"
                ),
                new SearchOperationsItem(
                    "3",
                    DateTime.Now.AddHours(-8).AddMinutes(-84),
                    OperationType.Income,
                    "5",
                    "1",
                    "Salary",
                    12520,
                    "USD",
                    "3",
                    "3"
                )
            },
            3
        );
        return Ok(result);
    }
}
