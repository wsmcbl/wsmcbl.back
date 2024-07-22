using wsmcbl.src.controller.api;

namespace wsmcbl.tests.integration;

public class CreateOfficialEnrollmentActionsIntegration
{
    [Fact]
    public async Task getStudentList()
    {
        var sut = Substitute.For<CreateOfficialEnrollmentActions>();

        var result = await sut.getStudentList();

        Assert.NotNull(result);
    }
}