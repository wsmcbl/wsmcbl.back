using wsmcbl.src.controller.api;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.integration;

public class CreateOfficialEnrollmentActionsIntegration
{
    [Fact]
    public async Task getStudentList()
    {
        await IntegrationTestSetup.SetupAsync();
        var sut = Substitute.For<CreateOfficialEnrollmentActions>();

        var result = await sut.getStudentList();

        Assert.NotNull(result);
    }

}