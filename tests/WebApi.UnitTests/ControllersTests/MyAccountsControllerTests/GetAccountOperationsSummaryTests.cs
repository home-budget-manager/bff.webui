namespace HomeBudgetManager.Bff.WebUI.WebApi.UnitTests.ControllersTests.MyAccountsControllerTests;

using HomeBudgetManager.Bff.WebUI.WebApi.Controllers.MyAccounts;

using Microsoft.AspNetCore.Mvc;

using ApiModel = HomeBudgetManager.Bff.WebUI.ServiceClients.Operations.V1.Summaries;

public class GetAccountOperationsSummaryTests : TestBase
{
    private string accountId = string.Empty;

    private IActionResult result = null!;

    [Fact]
    public void WhenAccountOperationsSummaryIsRetrievedThenResultIsCorrect()
    {
        this.Given(t => t.AccountIdIs(Guid.NewGuid().ToString()))
            .And(t => t.ControllerIsCreated())
            .And(t => t.SummariesAreMocked())
            .When(t => t.EndpointIsCalled())
            .Then(t => t.ResultIsOk())
            .And(t => t.ResultContainsOperationsSummary())
            .BDDfy();
    }

    private void AccountIdIs(string value) => this.accountId = value;

    private void SummariesAreMocked()
    {
        this.SummariesClientMock.Setup(c => c.GetSummariesAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ApiModel.GetSummariesResult(
                "USD",
                [
                    new ApiModel.SummaryItem("incomes", 1, 100.0m),
                    new ApiModel.SummaryItem("expenses", 1, 50.0m),
                    new ApiModel.SummaryItem("transfersIncoming", 1, 25.0m),
                    new ApiModel.SummaryItem("transfersOutgoing", 1, 25.0m)
                ]));
    }

    private async Task EndpointIsCalled()
    {
        this.result = await this.Controller.GetAccountOperationsSummary(this.accountId, CancellationToken.None);
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
