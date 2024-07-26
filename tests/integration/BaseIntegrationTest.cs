using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using wsmcbl.src.utilities;

namespace wsmcbl.tests.integration;

public abstract class BaseIntegrationTest : IClassFixture<WebApplicationFactory<PublicProgram>>
{
    protected string baseUri = "";
    protected readonly HttpClient client;
    private static string? TestDataFolder;

    protected BaseIntegrationTest(WebApplicationFactory<PublicProgram> factory)
    {
        client = factory.CreateClient();
        TestDataFolder = Path.Combine(AppContext.BaseDirectory, "..","..", "..", "resource");
    }

    private static StringContent getContent(string resource)
    {
        return new StringContent(resource, System.Text.Encoding.UTF8, "application/json");
    }

    protected static StringContent getContentByJson(string json)
    {
        return getContent(ReadJsonFromFile(json));
    }

    protected static StringContent getContentByDto(object? entity)
    {
        return getContent(JsonConvert.SerializeObject(entity));
    }

    protected static T? deserialize<T>(string content) => JsonConvert.DeserializeObject<T>(content);

    private static string ReadJsonFromFile(string fileName)
    {
        var filePath = Path.Combine(TestDataFolder!, fileName);
        return File.ReadAllText(filePath);
    }
}