namespace HomeBudgetManager.Bff.WebUI.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;

using HomeBudgetManager.Bff.WebUI.WebApi.Controllers.Accounts;

using Sdk = HomeBudgetManager.Services.Accounts.Sdk.Dictionaries;

[Route("api/[controller]")]
[ApiController]
public sealed class AccountsController(
    Sdk.IDictionariesClient dictionariesClient) : ControllerBase
{
    private readonly Sdk.IDictionariesClient dictionariesClient = dictionariesClient;

    [HttpGet("account-types")]
    public async Task<IResult> GetAccountTypes(
        CancellationToken cancellationToken)
    {
        var result = await this.dictionariesClient.GetAccountTypes(
            new(),
            cancellationToken);
        return result.Response.Match(
            r => Results.Ok(r.AccountTypes.Select(d => new AccountType(d.Id, d.Name)).ToArray()),
            e => Results.BadRequest(e.Message),
            e => Results.InternalServerError(e.Message));
    }

    [HttpPost("account-types")]
    public async Task<IResult> CreateAccountType(
        [FromBody] string accountType,
        CancellationToken cancellationToken)
    {
        var result = await this.dictionariesClient.CreateAccountType(
            new(accountType),
            cancellationToken);
        return result.Response.Match(
            r => Results.Created(),
            e => Results.BadRequest(e.Message),
            e => Results.InternalServerError(e.Message));
    }

    [HttpPut("account-types/{id}")]
    public async Task<IResult> UpdateAccountType(
        [FromRoute] int id,
        [FromBody] string accountType,
        CancellationToken cancellationToken)
    {
        var result = await this.dictionariesClient.UpdateAccountType(
            new(new(id, accountType)),
            cancellationToken);
        return result.Response.Match(
            r => Results.Ok(),
            e => Results.BadRequest(e.Message),
            e => Results.InternalServerError(e.Message));
    }
}
