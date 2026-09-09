namespace HomeBudgetManager.Bff.WebUI.WebApi.UnitTests.ControllersTests.MyAccountsTests;

using System.Collections.ObjectModel;

using HomeBudgetManager.Bff.WebUI.WebApi.Controllers.MyAccounts;

public class OperationsSummaryTests
{
    private readonly Collection<SummaryItem> items = [];

    private string currency = string.Empty;

    private OperationsSummary instance = null!;

    [Fact]
    public void WhenInstanceIsCreatedThenPropertiesAreSetCorrectly()
    {
        this.Given(t => t.CurrencyIs("USD"))
            .And(t => t.ItemIsAdded(new SummaryItem(SummaryItemType.Incomes, 5, 100.50m)))
            .And(t => t.ItemIsAdded(new SummaryItem(SummaryItemType.Expenses, 3, 75.25m)))
            .When(t => t.InstanceIsCreated())
            .Then(t => t.PropertiesAreSetCorrectly())
            .BDDfy();
    }

    private void CurrencyIs(string value) => this.currency = value;

    private void ItemIsAdded(SummaryItem item) => this.items.Add(item);

    private void InstanceIsCreated()
    {
        this.instance = new OperationsSummary(this.currency, [.. this.items]);
    }

    private void PropertiesAreSetCorrectly()
    {
        this.instance.Items.ShouldBeEquivalentTo(this.items.ToArray());
    }
}
