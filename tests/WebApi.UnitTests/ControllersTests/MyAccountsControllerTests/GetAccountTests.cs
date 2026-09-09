namespace HomeBudgetManager.Bff.WebUI.WebApi.UnitTests.ControllersTests.MyAccountsControllerTests;

using HomeBudgetManager.Bff.WebUI.WebApi.Controllers.MyAccounts;

using Microsoft.AspNetCore.Mvc;

public class GetAccountTests : TestBase
{
    private string accountId = string.Empty;

    private IActionResult result = null!;

    [Fact]
    public void WhenAccountsAreRetrivedThenResultIsCorrect()
    {
        this.Given(t => t.ControllerIsCreated())
            .And(t => t.AccountIdIs("1"))
            .When(t => t.EndpointIsCalled())
            .Then(t => t.ResultIsOk())
            .And(t => t.ResultContainsAccount())
            .BDDfy();
    }

    private void AccountIdIs(string value) => this.accountId = value;

    private void EndpointIsCalled()
    {
        this.result = this.Controller.GetAccount(this.accountId);
    }

    private void ResultIsOk()
    {
        this.result.ShouldBeOfType<OkObjectResult>();
    }

    private void ResultContainsAccount()
    {
        var okResult = this.result as OkObjectResult;
        okResult.ShouldNotBeNull();
        var account = okResult!.Value as AccountData;
        account.ShouldNotBeNull();
    }
}
