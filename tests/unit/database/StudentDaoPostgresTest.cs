using wsmcbl.src.database;
using wsmcbl.src.model.accounting;

namespace wsmcbl.tests.unit.database;

public class StudentDaoPostgresTest
{
    private readonly IStudentDao studentDaoPostgres;
    
    public StudentDaoPostgresTest()
    {
        var context = Substitute.For<PostgresContext>();
        studentDaoPostgres = new StudentDaoPostgres(context);
    }

    [Fact]
    public async Task getStudentList_ReturnsList()
    {
        var result = await studentDaoPostgres.getAll();
        Assert.NotEmpty(result);
    }
}