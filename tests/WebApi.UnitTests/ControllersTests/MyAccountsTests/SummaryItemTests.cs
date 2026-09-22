namespace HomeBudgetManager.Bff.WebUI.WebApi.UnitTests.ControllersTests.MyAccountsTests;

using HomeBudgetManager.Bff.WebUI.WebApi.Controllers.MyAccounts;

public class SummaryItemTests
{
    private SummaryItemType itemType;

    private int count;

    private decimal amount;

    private SummaryItem instance = null!;

    [Fact]
    public void WhenInstanceIsCreatedThenPropertiesAreSetCorrectly()
    {
        this.Given(t => t.ItemTypeIs(SummaryItemType.Incomes))
            .And(t => t.CountIs(5))
            .And(t => t.AmountIs(100.50m))
            .When(t => t.InstanceIsCreated())
            .Then(t => t.PropertiesAreSetCorrectly())
            .BDDfy();
    }

    private void ItemTypeIs(SummaryItemType value) => this.itemType = value;

    private void CountIs(int value) => this.count = value;

    private void AmountIs(decimal value) => this.amount = value;

    private void InstanceIsCreated()
    {
        this.instance = new SummaryItem(
            this.itemType,
            this.count,
            this.amount);
    }

    private void PropertiesAreSetCorrectly()
    {
        this.instance.ItemType.ShouldBe(this.itemType);
        this.instance.Count.ShouldBe(this.count);
        this.instance.Amount.ShouldBe(this.amount);
    }
}
