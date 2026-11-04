namespace HomeBudgetManager.Bff.WebUI.WebApi.Controllers.V2.Accounts;

public sealed class AccountType(
    int id,
    string name)
{
    public int Id { get; } = id;

    public string Name { get; } = name;
}
