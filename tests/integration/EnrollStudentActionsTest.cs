using Microsoft.AspNetCore.Mvc.Testing;
using wsmcbl.src.utilities;

namespace wsmcbl.tests.integration;

public class EnrollStudentActionsTest : BaseIntegrationTest
{
    public EnrollStudentActionsTest(WebApplicationFactory<PublicProgram> factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task getStudentList_ShouldReturnOkCode_WhenCalled()
    {
        const string uri = "/v1/secretary/enrollments/students";
        var response = await client.GetAsync(uri);
        
        response.EnsureSuccessStatusCode();
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }
}