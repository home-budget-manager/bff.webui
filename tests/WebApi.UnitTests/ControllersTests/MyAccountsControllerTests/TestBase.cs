namespace HomeBudgetManager.Bff.WebUI.WebApi.UnitTests.ControllersTests.MyAccountsControllerTests;

using HomeBudgetManager.Bff.WebUI.ServiceClients.Accounts;
using HomeBudgetManager.Bff.WebUI.ServiceClients.Operations.V1;
using HomeBudgetManager.Bff.WebUI.WebApi.Controllers;

public class TestBase
{
    private readonly Mock<IAccountsClient> accountsClientMock = new();

    private readonly Mock<IAccountBalanceClient> accountBalanceClientMock = new();

    private MyAccountsController controller = null!;

    protected MyAccountsController Controller => this.controller;

    protected Mock<IAccountsClient> AccountsClientMock => this.accountsClientMock;

    protected Mock<IAccountBalanceClient> AccountBalanceClientMock => this.accountBalanceClientMock;

    protected void ControllerIsCreated()
    {
        this.controller = new MyAccountsController(this.accountsClientMock.Object, this.accountBalanceClientMock.Object);
    }
}
