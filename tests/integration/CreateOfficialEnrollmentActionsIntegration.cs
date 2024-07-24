using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using wsmcbl.src.controller.api;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.integration;

public class CreateOfficialEnrollmentActionsIntegration : WebApplicationFactory<Program>
{
    protected override IWebHostBuilder CreateWebHostBuilder()
    {
        return base.CreateWebHostBuilder()
            .UseEnvironment("Test")
            .UseConfiguration(new ConfigurationBuilder()
                .AddJsonFile("appsettings.Test.json")
                .Build());
    }
    
    [Fact]
    public async Task getStudentList()
    {
        await IntegrationTestSetup.SetupAsync();
        var sut = Substitute.For<CreateOfficialEnrollmentActions>();

        var result = await sut.getStudentList();

        Assert.NotNull(result);
    }

}