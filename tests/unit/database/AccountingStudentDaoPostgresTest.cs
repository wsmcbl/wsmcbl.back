using wsmcbl.src.database;
using wsmcbl.src.exception;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.database;

public class AccountingStudentDaoPostgresTest : BaseDaoPostgresTest
{
    private AccountingStudentDaoPostgres? dao;

    [Fact]
    public async Task getById_StudentNotFount_ReturnsException()
    {
        context = TestDbContext.getInMemory();

        dao = new AccountingStudentDaoPostgres(context);

        await Assert.ThrowsAsync<EntityNotFoundException>(() => dao.getById("std-1"));
    }
}