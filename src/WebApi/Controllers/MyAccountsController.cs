namespace HomeBudgetManager.Bff.WebUI.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;

using HomeBudgetManager.Bff.WebUI.WebApi.Controllers.MyAccounts;

using System.Collections.ObjectModel;
using System.Security.Cryptography;
using HomeBudgetManager.Bff.WebUI.ServiceClients.Accounts;
using HomeBudgetManager.Bff.WebUI.ServiceClients.Operations.V1;
using HomeBudgetManager.Bff.WebUI.ServiceClients.Operations.V1.AccountBalance;

[Route("api/[controller]")]
[ApiController]
public class MyAccountsController : ControllerBase
{
    private readonly RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();

    private readonly IAccountsClient accountsClient;

    private readonly IAccountBalanceClient accountBalanceClient;

    public MyAccountsController(
        IAccountsClient accountsClient,
        IAccountBalanceClient accountBalanceClient)
    {
        this.accountsClient = accountsClient;
        this.accountBalanceClient = accountBalanceClient;
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
    public async Task<IActionResult> GetAccount(Guid accountId, CancellationToken cancellationToken)
    {
        var data = await this.accountsClient.GetAccountDetailsAsync(accountId, cancellationToken);
        var result = new AccountData(
            data.AccountId.ToString(),
            data.Name,
            MapAccountType(data.AccountType),
            data.Balance,
            data.CurrentPeriodChange,
            data.Currency,
            data.IsActive);
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
    public async Task<IActionResult> GetAccountBalanceHistory(
        Guid accountId,
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to,
        CancellationToken cancellationToken)
    {
        if (!from.HasValue)
        {
            var today = DateTime.UtcNow.Date;
            from = today.AddDays(-today.Day + 1);
        }

        from = from.Value.Date;
        if (!to.HasValue)
        {
            var today = DateTime.UtcNow.Date;
            to = today.AddDays(-today.Day + 1).AddMonths(1).AddDays(-1);
        }

        to = to.Value.Date;
        var result = await this.accountBalanceClient.GetBalanceHistoryAsync(
            accountId,
            new BalanceHistoryParameters(from, to),
            cancellationToken);
        var output = new AccountBalanceHistory(
            accountId,
            result.Currency,
            result.Items.Select(i => new BalanceHistoryEntry(i.Date, i.Balance)).ToArray());
        return this.Ok(output);

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
