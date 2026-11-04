namespace HomeBudgetManager.Bff.WebUI.WebApi.UnitTests.ControllersTests.V1Tests.MyAccountsControllerTests;

using HomeBudgetManager.Bff.WebUI.WebApi.Controllers.V1.MyAccounts;
using HomeBudgetManager.Services.Accounts.Sdk;
using HomeBudgetManager.Services.Accounts.Sdk.UserAccounts;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

public class GetAccountTests : TestBase
{
    private Guid accountId = Guid.Empty;

    private IResult result = null!;

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
        var expectedResult = new ResponseWrapper<GetUserAccountResponse>(
            new GetUserAccountResponse(new(
                this.accountId,
                "Test Account",
                new(1, "Checking"),
                "Description",
                DateTimeOffset.UtcNow,
                "USD",
                100.0m,
                true))); 
        this.UserAccountsClientMock.Setup(c => c.GetUserAccount(It.IsAny<GetUserAccountRequest>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(expectedResult));
    }

    private async Task EndpointIsCalled()
    {
        this.result = await this.Controller.GetAccount(this.accountId, CancellationToken.None);
    }

    private void ResultIsOk()
    {
        this.result.ShouldBeOfType<Ok<AccountData>>();
    }

    private void ResultContainsAccount()
    {
        var okResult = this.result as Ok<AccountData>;
        okResult.ShouldNotBeNull();
        var account = okResult!.Value;
        account.ShouldNotBeNull();
    }
}
