using System.Net.Http.Json;
using wsmcbl.src.model.secretary;

namespace wsmcbl.tests.integration;

public class UserControllerTests : CreateOfficialEnrollmentActionsIntegration
{
    private readonly HttpClient _client;

    public UserControllerTests()
    {
        _client = CreateClient();
    }

    [Fact]
    public async Task GetUsers_ReturnsAllUsers()
    {
        // Act
        var response = await _client.GetAsync("/api/users");

        // Assert
        response.EnsureSuccessStatusCode();
        var users = await response.Content.ReadFromJsonAsync<List<StudentEntity>>();
        Assert.NotNull(users);
        Assert.Equal(2, users.Count);
    }
}
