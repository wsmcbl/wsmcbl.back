using Newtonsoft.Json;
using wsmcbl.src.dto;
using wsmcbl.src.dto.secretary;

namespace wsmcbl.tests.integration;

public class TariffControllerTests
{
    [Fact]
    public async Task CreateTariff_()
    {
        // Arrange
        var dto = new TariffDataDto
        {
            concept = "Test Tariff",
            amount = 100.0f,
            dueDate = new DateOnlyDto(2023, 5, 1),
            typeId = 1,
            modality = 2
        };

        var uri = "http://localhost:5152/v1/secretary/configurations/schoolyears/tariffs";
        using (var client = new HttpClient())
        {
            // Act
            var response = await client
                .PostAsync(uri, new StringContent(JsonConvert.SerializeObject(dto), System.Text.Encoding.UTF8, "application/json"));

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}