using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using wsmcbl.src.utilities;

namespace wsmcbl.tests.integration;

public abstract class BaseIntegrationTest : IClassFixture<WebApplicationFactory<PublicProgram>>
{
    protected readonly HttpClient client;
    
    protected BaseIntegrationTest(WebApplicationFactory<PublicProgram> factory)
    {
        client = factory.CreateClient();
    }

    protected static StringContent getContentByDto(object? entity)
    {
        return new StringContent(JsonConvert.SerializeObject(entity), System.Text.Encoding.UTF8, "application/json");
    }

    protected static T? deserialize<T>(string content) => JsonConvert.DeserializeObject<T>(content);
}