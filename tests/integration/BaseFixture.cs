using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using wsmcbl.src.database.context;

namespace wsmcbl.tests.integration;

public class BaseFixture : WebApplicationFactory<Program>
{
    public HttpClient HttpClient { get; }

    protected BaseFixture()
    {
        HttpClient = CreateClient();
        
        var scopeFactory = Services.GetRequiredService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        
        var context = scope.ServiceProvider.GetRequiredService<PostgresContext>();
        seedData(context).GetAwaiter().GetResult();
        context.SaveChangesAsync();
    }

    protected virtual Task seedData(DbContext context) => Task.CompletedTask;
}