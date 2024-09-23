using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using wsmcbl.src.database.context;

namespace wsmcbl.tests.integration;

public class BaseFixture : WebApplicationFactory<Program>
{
    private PostgresContext dbContext { get; }
    public HttpClient HttpClient { get; }

    public BaseFixture()
    {
        HttpClient = CreateClient();
        
        var scopeFactory = Services.GetRequiredService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        
        dbContext = scope.ServiceProvider.GetRequiredService<PostgresContext>();
        seedData(dbContext).GetAwaiter().GetResult();
        dbContext.SaveChangesAsync();
    }

    protected virtual Task seedData(PostgresContext context) => Task.CompletedTask;
}