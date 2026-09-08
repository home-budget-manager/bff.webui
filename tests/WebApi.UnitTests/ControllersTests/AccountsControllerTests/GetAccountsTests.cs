namespace HomeBudgetManager.Bff.WebUI.WebApi.UnitTests.ControllersTests.AccountsControllerTests;

using HomeBudgetManager.Bff.WebUI.WebApi.Controllers.Accounts;

using Microsoft.AspNetCore.Mvc;

public class GetAccountsTests : TestBase
{
    private IActionResult result = null!;

    [Fact]
    public void WhenAccountsAreRetrivedThenResultIsCorrect()
    {
        this.Given(t => t.ControllerIsCreated())
            .When(t => t.EndpointIsCalled())
            .Then(t => t.ResultIsOk())
            .And(t => t.ResultContainsAccounts())
            .BDDfy();
    }

    private void EndpointIsCalled()
    {
        this.result = this.Controller.GetAccounts();
    }

    private void ResultIsOk()
    {
        this.result.ShouldBeOfType<OkObjectResult>();
    }

    private void ResultContainsAccounts()
    {
        var okResult = this.result as OkObjectResult;
        okResult.ShouldNotBeNull();
        var accounts = okResult!.Value as AccountData[];
        accounts.ShouldNotBeNull();
        accounts!.Length.ShouldBeGreaterThan(0);
    }
}
