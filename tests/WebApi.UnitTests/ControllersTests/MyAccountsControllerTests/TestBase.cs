namespace HomeBudgetManager.Bff.WebUI.WebApi.UnitTests.ControllersTests.MyAccountsControllerTests;

using HomeBudgetManager.Bff.WebUI.WebApi.Controllers;

public class TestBase
{
    private MyAccountsController controller = null!;

    protected MyAccountsController Controller => this.controller;

    protected void ControllerIsCreated()
    {
        this.controller = new MyAccountsController();
    }
}
