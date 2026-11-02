namespace HomeBudgetManager.Bff.WebUI.WebApi.UnitTests.ControllersTests.MyAccountsControllerTests;

using HomeBudgetManager.Bff.WebUI.ServiceClients.Accounts;
using HomeBudgetManager.Bff.WebUI.WebApi.Controllers.MyAccounts;

using Microsoft.AspNetCore.Mvc;

public class GetAccountTests : TestBase
{
    private Guid accountId = Guid.Empty;

    private IActionResult result = null!;

    [Fact]
    public void WhenAccountsAreRetrievedThenResultIsCorrect()
    {
        this.Given(t => t.ControllerIsCreated())
            .And(t => t.AccountIdIs(Guid.NewGuid()))
            .And(t => t.GetAccountIsMocked())
            .When(t => t.EndpointIsCalled())
            .Then(t => t.ResultIsOk())
            .And(t => t.ResultContainsAccount())
            .BDDfy();
    }

    private void AccountIdIs(Guid value) => this.accountId = value;

    private void GetAccountIsMocked()
    {
        this.AccountsClientMock.Setup(c => c.GetAccountDetailsAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new AccountDetails(
                this.accountId,
                "Test Account",
                "Checking",
                DateTimeOffset.UtcNow,
                "Description",
                100.0m,
                10.0m,
                "USD",
                true));
    }

    private async Task EndpointIsCalled()
    {
        this.result = await this.Controller.GetAccount(this.accountId, CancellationToken.None);
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
