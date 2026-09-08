namespace HomeBudgetManager.Bff.WebUI.WebApi.UnitTests.ControllersTests.AccountsControllerTests;

using HomeBudgetManager.Bff.WebUI.WebApi.Controllers;

public class TestBase
{
    private AccountsController controller = null!;

    protected AccountsController Controller => this.controller;

    protected void ControllerIsCreated()
    {
        this.controller = new AccountsController();
    }
}
