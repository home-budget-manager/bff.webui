namespace HomeBudgetManager.Bff.WebUI.WebApi.Controllers.V2.MyAccounts;

public class AccountData(
    string id,
    string name,
    AccountType accountType,
    decimal balance,
    decimal currentPeriodChange,
    string currency,
    bool isActive)
{
    public string Id { get; } = id;

    public string Name { get; } = name;

    public AccountType AccountType { get; } = accountType;

    public decimal Balance { get; } = balance;

    public decimal CurrentPeriodChange { get; } = currentPeriodChange;

    public string Currency { get; } = currency;

    public bool IsActive { get; } = isActive;
}
