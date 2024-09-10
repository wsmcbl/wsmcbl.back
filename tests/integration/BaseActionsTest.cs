using Newtonsoft.Json;
using wsmcbl.src.database.context;

namespace wsmcbl.tests.integration;

public abstract class BaseActionsTest<TClassFixture> : IClassFixture<TClassFixture>, IDisposable
    where TClassFixture : BaseClassFixture
{
    protected string baseUri = "";
    protected readonly HttpClient client;
    private static string? TestDataFolder;
    private readonly PostgresContext context;

    protected BaseActionsTest(TClassFixture factory)
    {
        client = factory.CreateClient();
        context = factory.getContext();
        TestDataFolder = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "resource");
    }
    
    private static StringContent getContent(string resource)
        => new(resource, System.Text.Encoding.UTF8, "application/json");
    protected static StringContent getContentByJson(string json) => getContent(ReadJsonFromFile(json));
    protected static StringContent getContentByDto(object? entity) 
        => getContent(JsonConvert.SerializeObject(entity));
    protected static T? deserialize<T>(string content) => JsonConvert.DeserializeObject<T>(content);

    private static string ReadJsonFromFile(string fileName)
    {
        var filePath = Path.Combine(TestDataFolder!, fileName);
        return File.ReadAllText(filePath);
    }

    public void Dispose()
    {
        client.Dispose();
        context.Dispose();
    }
}