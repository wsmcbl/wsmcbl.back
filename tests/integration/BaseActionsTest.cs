using Newtonsoft.Json;

namespace wsmcbl.tests.integration;

public abstract class BaseActionsTest<TClassFixture> : IClassFixture<TClassFixture> where TClassFixture : BaseFixture
{
    protected string baseUri = "";
    private readonly string resourcePath;
    protected HttpClient client { get; }

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
    
    protected static T? deserialize<T>(string content)
        => JsonConvert.DeserializeObject<T>(content);
    private string ReadJsonFromFile(string fileName)
        => File.ReadAllText(Path.Combine(resourcePath, fileName));
}