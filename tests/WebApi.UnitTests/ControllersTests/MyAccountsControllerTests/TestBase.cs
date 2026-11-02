namespace HomeBudgetManager.Bff.WebUI.WebApi.UnitTests.ControllersTests.MyAccountsControllerTests;

using HomeBudgetManager.Bff.WebUI.ServiceClients.Operations.V1;
using HomeBudgetManager.Bff.WebUI.WebApi.Controllers;

public class TestBase
{
    private readonly Mock<IAccountBalanceClient> accountBalanceClientMock = new();

    private readonly Mock<ISummariesClient> summariesClientMock = new();

    private readonly Mock<HomeBudgetManager.Services.Accounts.Sdk.UserAccounts.IUserAccountsClient> userAccountsClientMock = new();

    private MyAccountsController controller = null!;

    protected MyAccountsController Controller => this.controller;

    protected Mock<IAccountBalanceClient> AccountBalanceClientMock => this.accountBalanceClientMock;

    protected Mock<ISummariesClient> SummariesClientMock => this.summariesClientMock;

    protected Mock<HomeBudgetManager.Services.Accounts.Sdk.UserAccounts.IUserAccountsClient> UserAccountsClientMock => this.userAccountsClientMock;

    protected void ControllerIsCreated()
    {
        this.controller = new MyAccountsController(this.accountBalanceClientMock.Object, this.summariesClientMock.Object, this.userAccountsClientMock.Object);
    }
}
