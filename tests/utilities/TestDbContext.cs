using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;

namespace wsmcbl.tests.utilities;

public abstract class TestDbContext
{
    internal static PostgresContext getInMemory()
    {
        var options = new DbContextOptionsBuilder<PostgresContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
    
        return new PostgresContext(options);
    }
}