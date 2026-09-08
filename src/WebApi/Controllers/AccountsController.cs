namespace HomeBudgetManager.Bff.WebUI.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using HomeBudgetManager.Bff.WebUI.WebApi.Controllers.Accounts;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAccounts()
    {
        var result = new AccountData[]
        {
            new AccountData("1", "Checking Account", AccountType.Checking, 1000.00m, 50.00m, "USD", true),
            new AccountData("2", "Savings Account", AccountType.Savings, 5000.00m, 100.00m, "USD", true),
            new AccountData("3", "Credit Card", AccountType.CreditCard, -200.00m, -20.00m, "USD", false),
        };

        return Ok(result);
    }
}
