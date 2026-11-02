namespace HomeBudgetManager.Bff.WebUI.WebApi.UnitTests.ServiceClientsTests;

using System.Net;
using System.Net.Http;

using HomeBudgetManager.Bff.WebUI.ServiceClients.Operations.V1;

public class SummariesClientTests
{
    [Fact]
    public async Task WhenUpstreamReturnsAnErrorThenStatusCodeIsPreservedAndBodyIsNotExposed()
    {
        const string responseBody = "sensitive upstream response";
        using var httpClient = new HttpClient(new StubHttpMessageHandler(
            new HttpResponseMessage(HttpStatusCode.BadGateway)
            {
                Content = new StringContent(responseBody)
            }))
        {
            BaseAddress = new Uri("http://localhost/")
        };
        var client = new SummariesClient(httpClient);

        var exception = await Should.ThrowAsync<HttpRequestException>(
            () => client.GetSummariesAsync(Guid.NewGuid(), CancellationToken.None));

        exception.StatusCode.ShouldBe(HttpStatusCode.BadGateway);
        exception.Message.ShouldNotContain(responseBody);
    }

    private sealed class StubHttpMessageHandler(HttpResponseMessage response) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken) => Task.FromResult(response);
    }
}
