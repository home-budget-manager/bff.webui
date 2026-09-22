namespace HomeBudgetManager.Bff.WebUI.WebApi.UnitTests.ControllersTests.MyAccountsTests;

using System.Collections.ObjectModel;

using HomeBudgetManager.Bff.WebUI.WebApi.Controllers.MyAccounts;

public class AccountBalanceHistoryTests
{
    private readonly Collection<BalanceHistoryEntry> balanceHistory = new();

    private string accountId = string.Empty;

    private string currency = string.Empty;

    private AccountBalanceHistory instance = null!;

    [Fact]
    public void WhenInstanceIsCreatedThenPropertiesAreSetCorrectly()
    {
        this.Given(t => t.AccountIdIs("accountId"))
            .And(t => t.CurrencyIs("currency"))
            .And(t => t.EntryIsAdded(new DateTime(2024, 1, 1), 100.0m))
            .And(t => t.EntryIsAdded(new DateTime(2024, 1, 2), 200.0m))
            .When(t => t.InstanceIsCreated())
            .Then(t => t.PropertiesAreSetCorrectly())
            .BDDfy();
    }

    private void EntryIsAdded(DateTime date, decimal balance)
    {
        this.balanceHistory.Add(new BalanceHistoryEntry(date, balance));
    }

    private void AccountIdIs(string value) => this.accountId = value;

    private void CurrencyIs(string value) => this.currency = value;

    private void InstanceIsCreated()
    {
        this.instance = new AccountBalanceHistory(this.accountId, this.currency, this.balanceHistory.ToArray());
    }

    private void PropertiesAreSetCorrectly()
    {
        this.instance.AccountId.ShouldBe(this.accountId);
        this.instance.Currency.ShouldBe(this.currency);
        this.instance.BalanceHistory.ShouldBeEquivalentTo(this.balanceHistory.ToArray());
    }
}
