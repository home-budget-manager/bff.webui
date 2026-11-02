namespace HomeBudgetManager.Bff.WebUI.WebApi.UnitTests.ControllersTests.MyAccountsControllerTests;

using HomeBudgetManager.Bff.WebUI.ServiceClients.Accounts;
using HomeBudgetManager.Bff.WebUI.WebApi.Controllers.MyAccounts;
using HomeBudgetManager.Services.Accounts.Sdk;
using HomeBudgetManager.Services.Accounts.Sdk.UserAccounts;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

public class GetAccountsTests : TestBase
{
    private IResult result = null!;

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
        var expectedResponse = new ResponseWrapper<GetUserAccountsResponse>(new GetUserAccountsResponse([
                new GetUserAccountsResponse.Account(Guid.NewGuid(), "Checking Account", new(1, "Checking"), "USD", 1000.00m),
                new GetUserAccountsResponse.Account(Guid.NewGuid(), "Savings Account", new(2, "Savings"), "USD", 5000.00m),
                new GetUserAccountsResponse.Account(Guid.NewGuid(), "Credit Card", new(3, "CreditCard"), "USD", -200.00m),
            ]));
        this.UserAccountsClientMock.Setup(ac => ac.GetUserAccounts(It.IsAny<GetUserAccountsRequest>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(expectedResponse));
    }

    private async Task EndpointIsCalled()
    {
        this.result = await this.Controller.GetAccounts(CancellationToken.None);
    }

    private void ResultIsOk()
    {
        this.result.ShouldBeOfType<Ok<AccountData[]>>();
    }

    private void ResultContainsAccounts()
    {
        var okResult = this.result as Ok<AccountData[]>;
        okResult.ShouldNotBeNull();
        var accounts = okResult!.Value;
        accounts.ShouldNotBeNull();
        accounts!.Length.ShouldBeGreaterThan(0);
    }
}
