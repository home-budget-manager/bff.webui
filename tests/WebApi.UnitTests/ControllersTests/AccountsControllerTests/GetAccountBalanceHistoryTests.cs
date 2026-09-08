namespace HomeBudgetManager.Bff.WebUI.WebApi.UnitTests.ControllersTests.AccountsControllerTests;

using HomeBudgetManager.Bff.WebUI.WebApi.Controllers.Accounts;

using Microsoft.AspNetCore.Mvc;

public class GetAccountBalanceHistoryTests : TestBase
{
    private string accountId = string.Empty;

    private IActionResult result = null!;

    [Fact]
    public void WhenAccountBalanceHistoryIsRetrivedThenResultIsCorrect()
    {
        this.Given(t => t.AccountIdIs("429"))
            .And(t => t.ControllerIsCreated())
            .When(t => t.EndpointIsCalled())
            .Then(t => t.ResultIsOk())
            .And(t => t.ResultContainsAccountBalanceHistory())
            .BDDfy();
    }


    private void AccountIdIs(string value) => this.accountId = value;

    private void EndpointIsCalled()
    {
        this.result = this.Controller.GetAccountBalanceHistory(this.accountId, null, null);
    }

    private void ResultIsOk()
    {
        this.result.ShouldBeOfType<OkObjectResult>();
    }

    private void ResultContainsAccountBalanceHistory()
    {
        var okResult = this.result as OkObjectResult;
        okResult.ShouldNotBeNull();
        var accountBalanceHistory = okResult!.Value as AccountBalanceHistory;
        accountBalanceHistory.ShouldNotBeNull();
        accountBalanceHistory.BalanceHistory.Length.ShouldBeGreaterThan(0);
    }
}
