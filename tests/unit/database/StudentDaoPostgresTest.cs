using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database;
using wsmcbl.src.model.accounting;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.database;

public class StudentDaoPostgresTest
{
    private readonly PostgresContext context = Substitute
        .For<PostgresContext>(new DbContextOptions<PostgresContext>());

    [Fact]
    public async Task getStudentList_ReturnsList()
    {
        var entityGenerator = new TestEntityGenerator();
        var list = entityGenerator.aStudentList();
        
        var entities = TestDbSet<StudentEntity>.getFake(list);
        context.Set<StudentEntity>().Returns(entities);
        var dao = new StudentDaoPostgres(context);
        
        var result = await dao.getAll();
        
        Assert.NotEmpty(result);
        Assert.Equal(list, result);
    }
}