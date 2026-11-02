namespace HomeBudgetManager.Bff.WebUI.WebApi.UnitTests.ControllersTests.MyAccountsControllerTests;

using HomeBudgetManager.Bff.WebUI.ServiceClients.Operations.V1.AccountBalance;
using HomeBudgetManager.Bff.WebUI.WebApi.Controllers.MyAccounts;

using Microsoft.AspNetCore.Mvc;

public class GetAccountBalanceHistoryTests : TestBase
{
    private Guid accountId = Guid.Empty;

    private IActionResult result = null!;

    [Fact]
    public void WhenAccountBalanceHistoryIsRetrievedThenResultIsCorrect()
    {
        this.Given(t => t.AccountIdIs(Guid.NewGuid()))
            .And(t => t.ControllerIsCreated())
            .And(t => t.AccountBalanceIsMocked())
            .When(t => t.EndpointIsCalled())
            .Then(t => t.ResultIsOk())
            .And(t => t.ResultContainsAccountBalanceHistory())
            .BDDfy();
    }


    private void AccountIdIs(Guid value) => this.accountId = value;

    private void AccountBalanceIsMocked()
    {
        this.AccountBalanceClientMock.Setup(c => c.GetBalanceHistoryAsync(It.IsAny<Guid>(), It.IsAny<BalanceHistoryParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new BalanceHistoryResult(
                new[] { new BalanceHistoryItem(DateTime.UtcNow, 100.0m) },
                "USD",
                0,
                10));
    }

    private async Task EndpointIsCalled()
    {
        this.result = await this.Controller.GetAccountBalanceHistory(this.accountId, null, null, CancellationToken.None);
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
