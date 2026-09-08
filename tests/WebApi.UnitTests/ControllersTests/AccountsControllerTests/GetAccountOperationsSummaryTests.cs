namespace HomeBudgetManager.Bff.WebUI.WebApi.UnitTests.ControllersTests.AccountsControllerTests;

using HomeBudgetManager.Bff.WebUI.WebApi.Controllers.Accounts;

using Microsoft.AspNetCore.Mvc;

public class GetAccountOperationsSummaryTests : TestBase
{
    private string accountId = string.Empty;

    private IActionResult result = null!;

    [Fact]
    public void WhenAccountOperationsSummaryIsRetrivedThenResultIsCorrect()
    {
        this.Given(t => t.AccountIdIs("429"))
            .And(t => t.ControllerIsCreated())
            .When(t => t.EndpointIsCalled())
            .Then(t => t.ResultIsOk())
            .And(t => t.ResultContainsOperationsSummary())
            .BDDfy();
    }

    private void AccountIdIs(string value) => this.accountId = value;

    private void EndpointIsCalled()
    {
        this.result = this.Controller.GetAccountOperationsSummary(this.accountId);
    }

    private void ResultIsOk()
    {
        this.result.ShouldBeOfType<OkObjectResult>();
    }

    private void ResultContainsOperationsSummary()
    {
        var okResult = this.result as OkObjectResult;
        okResult.ShouldNotBeNull();
        var operationsSummary = okResult!.Value as OperationsSummary;
        operationsSummary.ShouldNotBeNull();
        operationsSummary.Items.Length.ShouldBe(4);
        operationsSummary.Items.ShouldContain(si => si.ItemType == SummaryItemType.Incomes);
        operationsSummary.Items.ShouldContain(si => si.ItemType == SummaryItemType.Expenses);
        operationsSummary.Items.ShouldContain(si => si.ItemType == SummaryItemType.TransfersIncoming);
        operationsSummary.Items.ShouldContain(si => si.ItemType == SummaryItemType.TransfersOutgoing);
    }
}
