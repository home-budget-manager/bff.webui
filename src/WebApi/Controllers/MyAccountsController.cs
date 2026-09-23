namespace HomeBudgetManager.Bff.WebUI.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;

using HomeBudgetManager.Bff.WebUI.WebApi.Controllers.MyAccounts;

using System.Collections.ObjectModel;
using System.Security.Cryptography;
using HomeBudgetManager.Bff.WebUI.ServiceClients.Accounts;

[Route("api/[controller]")]
[ApiController]
public class MyAccountsController : ControllerBase
{
    private readonly RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();

    private readonly IAccountsClient accountsClient;

    public MyAccountsController(
        IAccountsClient accountsClient)
    {
        this.accountsClient = accountsClient;
    }

    [HttpGet]
    public async Task<IActionResult> GetAccounts(CancellationToken cancellationToken)
    {
        var data = await this.accountsClient.GetUserAccountsAsync(cancellationToken);

        var result = data.Select(d => new AccountData(
            d.AccountId.ToString(),
            d.Name,
            MapAccountType(d.AccountType),
            d.Balance,
            d.CurrentPeriodChange,
            d.Currency,
            d.IsActive)).ToArray();


        return this.Ok(result);
    }

    [HttpGet("{accountId}")]
    public IActionResult GetAccount(string accountId)
    {
        var result = new AccountData(
            accountId,
            "Account name from API",
            AccountType.Checking,
            14_543.23M,
            1_234.56M,
            "USD",
            true);
        return this.Ok(result);
    }

    [HttpGet("{accountId}/operationsSummary")]
    public IActionResult GetAccountOperationsSummary(string accountId)
    {
        var result = new OperationsSummary(
            "USD",
            [
                new SummaryItem(SummaryItemType.Incomes, 4, 5050M),
                new SummaryItem(SummaryItemType.Expenses, 3, -1640.91M),
                new SummaryItem(SummaryItemType.TransfersIncoming, 1, 28.5M),
                new SummaryItem(SummaryItemType.TransfersOutgoing, 1, -1028.5M)
            ]);
        return this.Ok(result);
    }

    [HttpGet("{accountId}/balanceHistory")]
    public IActionResult GetAccountBalanceHistory(string accountId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
    {
        if (!startDate.HasValue)
        {
            startDate = DateTime.UtcNow.AddDays(-20);
        }

        startDate = startDate.Value.Date;
        if (!endDate.HasValue)
        {
            endDate = DateTime.UtcNow.Date;
        }

        endDate = endDate.Value.Date;
        var entries = new Collection<BalanceHistoryEntry>();
        var currentBalance = 12345.45M;
        for (var currentDate = startDate.Value; currentDate <= endDate.Value; currentDate = currentDate.AddDays(1))
        {
            entries.Add(new BalanceHistoryEntry(currentDate, currentBalance));
            var randomBytes = new byte[4];
            this.randomNumberGenerator.GetBytes(randomBytes);
            var randomValue = BitConverter.ToInt32(randomBytes, 0);
            var dailyChange = (randomValue % 40000 - 20000) / 100M;
            currentBalance += dailyChange;
        }

        var result = new AccountBalanceHistory(
            accountId,
            "USD",
            [.. entries]);
        return this.Ok(result);

    }

    private static AccountType MapAccountType(string accountType)
    {
        return accountType switch
        {
            "Checking" => AccountType.Checking,
            "Savings" => AccountType.Savings,
            "CreditCard" => AccountType.CreditCard,
            _ => throw new InvalidOperationException($"Unknown account type: {accountType}")
        };
    }
}
