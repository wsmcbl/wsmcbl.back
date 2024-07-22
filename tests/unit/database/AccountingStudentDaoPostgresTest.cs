using wsmcbl.src.database;
using wsmcbl.src.exception;
using wsmcbl.src.model.accounting;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.database;

public class AccountingStudentDaoPostgresTest : BaseDaoPostgresTest
{
    private AccountingStudentDaoPostgres? dao;

    [Fact]
    public async Task getStudentList_ReturnsList()
    {
        var list = TestEntityGenerator.aStudentList();

        context = TestDbContext.getInMemory();
        context.Set<StudentEntity>().AddRange(list);
        await context.SaveChangesAsync();
        
        dao = new AccountingStudentDaoPostgres(context);

        var result = await dao.getAll();

        Assert.NotEmpty(result);
        Assert.Equivalent(list, result);
    }

    [Fact]
    public async Task getStudentList_EmptyList()
    {
        var entities = TestDbSet<StudentEntity>.getFake([]);
        context.Set<StudentEntity>().Returns(entities);
        dao = new AccountingStudentDaoPostgres(context);

        var result = await dao.getAll();

        Assert.Empty(result);
    }


    [Fact]
    public async Task getById_ReturnsStudent()
    {
        var student = TestEntityGenerator.aStudent("std-1");
        var tariff = TestEntityGenerator.aTariff();
        
        context = TestDbContext.getInMemory();
        await context.Set<StudentEntity>().AddAsync(student);
        await context.Set<TariffEntity>().AddAsync(tariff);
        await context.SaveChangesAsync();

        dao = new AccountingStudentDaoPostgres(context);

        var result = await dao.getById("std-1");

        Assert.NotNull(result);
        Assert.Equal(student, result);
    }

    [Fact]
    public async Task getById_StudentNotFount_ReturnsException()
    {
        context = TestDbContext.getInMemory();

        dao = new AccountingStudentDaoPostgres(context);

        await Assert.ThrowsAsync<EntityNotFoundException>(() => dao.getById("std-1"));
    }
}