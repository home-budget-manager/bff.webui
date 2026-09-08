namespace HomeBudgetManager.Bff.WebUI.WebApi.UnitTests.ControllersTests.AccountsTests;

using System.Collections.ObjectModel;

using HomeBudgetManager.Bff.WebUI.WebApi.Controllers.Accounts;

public class OperationsSummaryTests
{
    private readonly Collection<SummaryItem> items = [];

    private OperationsSummary instance = null!;

    [Fact]
    public void WhenInstanceIsCreatedThenPropertiesAreSetCorrectly()
    {
        this.Given(t => t.ItemIsAdded(new SummaryItem(SummaryItemType.Incomes, 5, 100.50m, "USD")))
            .And(t => t.ItemIsAdded(new SummaryItem(SummaryItemType.Expenses, 3, 75.25m, "USD")))
            .When(t => t.InstanceIsCreated())
            .Then(t => t.PropertiesAreSetCorrectly())
            .BDDfy();
    }

    private void ItemIsAdded(SummaryItem item) => this.items.Add(item);

    private void InstanceIsCreated()
    {
        this.instance = new OperationsSummary(this.items.ToArray());
    }

    private void PropertiesAreSetCorrectly()
    {
        this.instance.Items.ShouldBeEquivalentTo(this.items.ToArray());
    }
}
