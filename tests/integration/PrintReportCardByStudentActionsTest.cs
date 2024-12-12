using wsmcbl.src.dto.academy;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.integration;

[CollectionDefinition("SequentialCollection")]
public class PrintReportCardByStudentActionsTest : BaseActionsTest<PrintReportCardByStudentFixture>
{
    public PrintReportCardByStudentActionsTest(PrintReportCardByStudentFixture factory) : base(factory)
    {
        baseUri = "/v3/academy";
    }
    
    [Fact]
    public async Task getStudentInformation_ShouldReturnJson_WhenCalled()
    {
        var response = await client.GetAsync($"{baseUri}/students/hola");

        await response.EnsureSuccess();
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        Assert.NotNull(content);

        var list = deserialize<StudentScoreInformationDto>(content);
        Assert.NotNull(list);
    }
}