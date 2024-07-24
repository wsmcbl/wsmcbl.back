using Microsoft.AspNetCore.Mvc.Testing;
using wsmcbl.src;

namespace wsmcbl.tests.integration;

public class ApiIntegrationTests : IClassFixture<WebApplicationFactory<TestProgram>>
{
    private readonly HttpClient _client;

    public ApiIntegrationTests(WebApplicationFactory<TestProgram> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_Endpoint_Returns_Success()
    {
        // Act
        var response = await _client.GetAsync("/v1/accounting/students");

        // Assert
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        Assert.Contains("expected content", responseString);
    }
}