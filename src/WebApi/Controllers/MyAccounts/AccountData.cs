namespace HomeBudgetManager.Bff.WebUI.WebApi.Controllers.MyAccounts;

public class AccountData
{
    public AccountData(
        string id,
        string name,
        AccountType type,
        decimal balance,
        decimal currentPeriodChange,
        string currency,
        bool isActive)
    {
        this.Id = id;
        this.Name = name;
        this.Type = type;
        this.Balance = balance;
        this.CurrentPeriodChange = currentPeriodChange;
        this.Currency = currency;
        this.IsActive = isActive;
    }

    public string Id { get; }

    public string Name { get; }

    public AccountType Type { get; }

    public decimal Balance { get; }

    public decimal CurrentPeriodChange { get; }

    public string Currency { get; }

    public bool IsActive { get; }
}
