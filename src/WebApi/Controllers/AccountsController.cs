namespace HomeBudgetManager.Bff.WebUI.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;

using HomeBudgetManager.Bff.WebUI.WebApi.Controllers.Accounts;

using AccountsSdk = HomeBudgetManager.Services.Accounts.Sdk;
using HomeBudgetManager.Services.Accounts.Sdk.Dictionaries;

[Route("api/[controller]")]
[ApiController]
public sealed class AccountsController(
    IDictionariesClient dictionariesClient) : ControllerBase
{
    private readonly IDictionariesClient dictionariesClient = dictionariesClient;

    [HttpGet("account-types")]
    public async Task<IResult> GetAccountTypes(CancellationToken cancellationToken)
    {
        var result = await this.dictionariesClient.GetAccountTypes(new(), cancellationToken);
        return result.Response.Match(
            r => Results.Ok(r.AccountTypes.Select(d => new AcccountType(d.Id, d.Name)).ToArray()),
            e => Results.BadRequest(e.Message),
            e => Results.InternalServerError(e.Message));
    }
}
