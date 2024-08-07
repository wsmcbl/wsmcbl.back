using Microsoft.EntityFrameworkCore;
using NSubstitute;
using wsmcbl.src.database.context;

namespace wsmcbl.tests.unit.database;

public class BaseDaoPostgresTest
{
    protected PostgresContext context =
        Substitute.For<PostgresContext>(new DbContextOptions<PostgresContext>());
}