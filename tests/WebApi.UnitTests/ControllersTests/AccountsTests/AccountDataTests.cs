namespace HomeBudgetManager.Bff.WebUI.WebApi.UnitTests.ControllersTests.AccountsTests;

using HomeBudgetManager.Bff.WebUI.WebApi.Controllers.Accounts;

public class AccountDataTests
{
    private string id = "account-id";

    private string name = "account-name";

    private AccountType accountType = AccountType.Checking;

    private decimal balance = 100.50m;

    private decimal currentPeriodChange = 10.25m;

    private string currency = "USD";

    private bool isActive = true;

    private AccountData instance = null!;

    [Fact]
    public void WhenAccountDataIsCreatedThenValuesAreSetCorrectly()
    {
        this.Given(t => t.IdIs(this.id))
            .And(t => t.NameIs(this.name))
            .And(t => t.AccountTypeIs(this.accountType))
            .And(t => t.BalanceIs(this.balance))
            .And(t => t.CurrentPeriodChangeIs(this.currentPeriodChange))
            .And(t => t.CurrencyIs(this.currency))
            .And(t => t.IsActiveIs(this.isActive))
            .When(t => t.InstanceIsCreated())
            .Then(t => t.PropertiesHaveCorrectValues())
            .BDDfy();
    }

    private void IdIs(string value) => this.id = value;

    private void NameIs(string value) => this.name = value;

    private void AccountTypeIs(AccountType value) => this.accountType = value;

    private void BalanceIs(decimal value) => this.balance = value;

    private void CurrentPeriodChangeIs(decimal value) => this.currentPeriodChange = value;

    private void CurrencyIs(string value) => this.currency = value;

    private void IsActiveIs(bool value) => this.isActive = value;

    private void InstanceIsCreated()
    {
        this.instance = new AccountData(
            this.id,
            this.name,
            this.accountType,
            this.balance,
            this.currentPeriodChange,
            this.currency,
            this.isActive);
    }

    private void PropertiesHaveCorrectValues()
    {
        this.instance.Id.ShouldBe(this.id);
        this.instance.Name.ShouldBe(this.name);
        this.instance.Type.ShouldBe(this.accountType);
        this.instance.Balance.ShouldBe(this.balance);
        this.instance.CurrentPeriodChange.ShouldBe(this.currentPeriodChange);
        this.instance.Currency.ShouldBe(this.currency);
        this.instance.IsActive.ShouldBe(this.isActive);
    }
}
