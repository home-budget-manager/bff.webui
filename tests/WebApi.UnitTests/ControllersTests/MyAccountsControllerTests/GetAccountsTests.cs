namespace HomeBudgetManager.Bff.WebUI.WebApi.UnitTests.ControllersTests.MyAccountsControllerTests;

using HomeBudgetManager.Bff.WebUI.ServiceClients.Accounts;
using HomeBudgetManager.Bff.WebUI.WebApi.Controllers.MyAccounts;

using Microsoft.AspNetCore.Mvc;

public class GetAccountsTests : TestBase
{
    private IActionResult result = null!;

    [Fact]
    public void WhenAccountsAreRetrievedThenResultIsCorrect()
    {
        this.Given(t => t.AccountsListIsMocked())
            .And(t => t.ControllerIsCreated())
            .When(t => t.EndpointIsCalled())
            .Then(t => t.ResultIsOk())
            .And(t => t.ResultContainsAccounts())
            .BDDfy();
    }

    private void AccountsListIsMocked()
    {
        this.AccountsClientMock.Setup(ac => ac.GetUserAccountsAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<IReadOnlyCollection<AccountInfo>>(new List<AccountInfo>
            {
                new(Guid.NewGuid(), "Checking Account", "Checking", 1000.00m, 50.00m, "USD", true),
                new(Guid.NewGuid(), "Savings Account", "Savings", 5000.00m, 100.00m, "USD", true),
                new(Guid.NewGuid(), "Credit Card", "CreditCard", -200.00m, -20.00m, "USD", false),
            }));
    }

    private async Task EndpointIsCalled()
    {
        this.result = await this.Controller.GetAccounts(CancellationToken.None);
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
