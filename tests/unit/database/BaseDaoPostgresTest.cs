using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database;

namespace wsmcbl.tests.unit.database;

public class BaseDaoPostgresTest
{
    protected PostgresContext context =
        Substitute.For<PostgresContext>(new DbContextOptions<PostgresContext>());
    
}