using Newtonsoft.Json;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.integration;

public abstract class BaseActionsTest<TClassFixture> : IClassFixture<TClassFixture> where TClassFixture : BaseFixture
{
    protected string baseUri = "";
    protected readonly HttpClient client;
    
    private readonly string resourcePath;
    
    protected BaseActionsTest(TClassFixture factory)
    {
        client = factory.HttpClient;
        resourcePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "resource");
    }
    
    private static StringContent getContent(string resource)
        => new(resource, System.Text.Encoding.UTF8, "application/json");
    protected StringContent getContentByJson(string json)
        => getContent(ReadJsonFromFile(json));
    protected static StringContent getContentByDto(object? entity)
        => getContent(JsonConvert.SerializeObject(entity));

    protected static T? deserialize<T>(string content) => Utilities.deserialize<T>(content);
    private string ReadJsonFromFile(string fileName)
        => File.ReadAllText(Path.Combine(resourcePath, fileName));
}