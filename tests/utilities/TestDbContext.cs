using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database;

namespace wsmcbl.tests.utilities;

public abstract class TestDbContext
{
    internal static PostgresContext getInMemory(string name)
    {
        var options = new DbContextOptionsBuilder<PostgresContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
    
        return new PostgresContext(options);
    }
}