using Microsoft.AspNetCore.Mvc.Testing;
using wsmcbl.src.dto;
using wsmcbl.src.dto.secretary;
using wsmcbl.src.utilities;

namespace wsmcbl.tests.integration;

public class EnrollStudentActionsTest : BaseIntegrationTest
{
    public EnrollStudentActionsTest(WebApplicationFactory<PublicProgram> factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task Get_Endpoint_Returns_Success()
    {
        var dto = new TariffDataDto
        {
            concept = "Nueva tariff, desde las pruebas",
            amount = 100.0f,
            dueDate = new DateOnlyDto(2023, 5, 1),
            typeId = 1,
            modality = 2
        };
        
        const string uri = "/v1/secretary/configurations/schoolyears/tariffs";
        var response = await client.PostAsync(uri, getContentByDto(dto));
        
        response.EnsureSuccessStatusCode();
        Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
    }
}