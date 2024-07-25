using Microsoft.AspNetCore.Mvc.Testing;
using wsmcbl.src.utilities;

namespace wsmcbl.tests.integration;

public class ApiIntegrationTests : IClassFixture<WebApplicationFactory<PublicProgram>>
{
    private readonly HttpClient _client;

    public ApiIntegrationTests(WebApplicationFactory<PublicProgram> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_Endpoint_Returns_Success()
    {
        var response = await _client.GetAsync("/v1/accounting/students");
        
        response.EnsureSuccessStatusCode();
    }
}