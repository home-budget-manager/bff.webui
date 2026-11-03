namespace HomeBudgetManager.Bff.WebUI.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;

using HomeBudgetManager.Bff.WebUI.ServiceClients.Operations.V1;
using HomeBudgetManager.Bff.WebUI.ServiceClients.Operations.V1.AccountBalance;
using HomeBudgetManager.Bff.WebUI.WebApi.Controllers.MyAccounts;

using AccountsSdk = HomeBudgetManager.Services.Accounts.Sdk;
using GetSummariesResult = HomeBudgetManager.Bff.WebUI.ServiceClients.Operations.V1.Summaries.GetSummariesResult;

[Route("api/[controller]")]
[ApiController]
public class MyAccountsController(
    IAccountBalanceClient accountBalanceClient,
    ISummariesClient summariesClient,
    AccountsSdk.UserAccounts.IUserAccountsClient client) : ControllerBase
{
    private const string GetAccountDetailsRouteName = "GetAccountDetails";

    private readonly IAccountBalanceClient accountBalanceClient = accountBalanceClient;

    private readonly ISummariesClient summariesClient = summariesClient;

    private readonly AccountsSdk.UserAccounts.IUserAccountsClient client = client;

    [HttpPost]
    public async Task<IResult> CreateAccount([FromBody] CreateAccountInput input, CancellationToken cancellationToken)
    {
        var result = await this.client.Create(new AccountsSdk.UserAccounts.CreateAccountRequest(
            input.Name,
            input.AccountTypeId,
            input.Description,
            input.Currency), cancellationToken);
        return result.Response.Match(
            r => Results.CreatedAtRoute(
                GetAccountDetailsRouteName,
                new { accountId = r.AccountId }, new { accountId = r.AccountId }),
            e => Results.BadRequest(e.Message),
            e => Results.InternalServerError(e.Message));
    }

    [HttpGet]
    public async Task<IResult> GetAccounts(CancellationToken cancellationToken)
    {
        var result = await this.client.GetUserAccounts(new(), cancellationToken);

        return result.Response.Match(
            r => Results.Ok(r.Accounts.Select(d => new AccountData(
                d.Id.ToString(),
                d.Name,
                new(d.AccountType.Id, d.AccountType.Name),
                d.Balance,
                0,
                d.Currency,
                d.IsActive)).ToArray()),
            e => Results.BadRequest(e.Message),
            e => Results.InternalServerError(e.Message));
    }

    [HttpGet("{accountId}", Name = GetAccountDetailsRouteName)]
    public async Task<IResult> GetAccount(Guid accountId, CancellationToken cancellationToken)
    {
        var data = await this.client.GetUserAccount(new(accountId), cancellationToken);
        return data.Response.Match(
            r => Results.Ok(new AccountData(
                r.Details.Id.ToString(),
                r.Details.Name,
                new(r.Details.AccountType.Id, r.Details.AccountType.Name),
                r.Details.Balance,
                0,
                r.Details.Currency,
                r.Details.IsActive)),
            e => Results.BadRequest(e.Message),
            e => Results.InternalServerError(e.Message));
    }

    [HttpGet("{accountId}/operationsSummary")]
    public async Task<IActionResult> GetAccountOperationsSummary(Guid accountId, CancellationToken cancellationToken)
    {
        GetSummariesResult? data;
        try
        {
            data = await this.summariesClient.GetSummariesAsync(accountId, cancellationToken);
        }
        catch (HttpRequestException exception) when (exception.StatusCode.HasValue)
        {
            return this.StatusCode((int)exception.StatusCode.Value);
        }

        if (data is null)
        {
            return this.NotFound();
        }

        var result = new OperationsSummary(
            data.Currency,
            [.. data.Summaries.Select(s => new SummaryItem(
                MapSummaryItemType(s.ItemType),
                s.Count,
                s.Amount))]);
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
            [.. result.Items.Select(i => new BalanceHistoryEntry(i.Date, i.Balance))]);
        return this.Ok(output);

    }

    private static SummaryItemType MapSummaryItemType(string itemType)
    {
        return itemType switch
        {
            "incomes" => SummaryItemType.Incomes,
            "expenses" => SummaryItemType.Expenses,
            "transfersIncoming" => SummaryItemType.TransfersIncoming,
            "transfersOutgoing" => SummaryItemType.TransfersOutgoing,
            _ => throw new InvalidOperationException($"Unknown summary item type: {itemType}")
        };
    }
}
