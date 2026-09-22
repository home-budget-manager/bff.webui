namespace HomeBudgetManager.Bff.WebUI.WebApi.UnitTests.ControllersTests.MyAccountsTests;

using HomeBudgetManager.Bff.WebUI.WebApi.Controllers.MyAccounts;

public class BalanceHistoryEntryTests
{
    private DateTime date;

    private decimal balance;

    private BalanceHistoryEntry instance = null!;

    [Fact]
    public void WhenInstanceIsCreatedThenPropertiesAreSetCorrectly()
    {
        this.Given(t => t.DateIs(new DateTime(2024, 6, 1)))
            .And(t => t.BalanceIs(1000.50m))
            .When(t => t.InstanceIsCreated())
            .Then(t => t.PropertiesAreSetCorrectly())
            .BDDfy();
    }

    private void DateIs(DateTime value) => this.date = value;

    private void BalanceIs(decimal value) => this.balance = value;

    private void InstanceIsCreated()
    {
        this.instance = new BalanceHistoryEntry(
            this.date,
            this.balance);
    }

    private void PropertiesAreSetCorrectly()
    {
        this.instance.Date.ShouldBe(this.date);
        this.instance.Balance.ShouldBe(this.balance);
    }
}
