using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace wsmcbl.tests.integration;

public class IntegrationTests : WebApplicationFactory<Program>
{
    
    protected override IWebHostBuilder CreateWebHostBuilder()
    {
        return base.CreateWebHostBuilder()
            .UseEnvironment("Test");
    }

    [Fact]
    public async Task GetEndpoint_ReturnsSuccessfulResponse()
    {
        // Arrange
        var client = CreateClient();

        // Act
        var response = await client.GetAsync("/v1/accounting/students");

        // Assert
        response.EnsureSuccessStatusCode();
        // Agrega más aserciones según tus necesidades
    }
}