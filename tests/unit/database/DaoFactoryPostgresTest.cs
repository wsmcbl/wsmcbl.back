using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database;
using wsmcbl.src.database.context;
using wsmcbl.src.model.accounting;
using SecretaryStudentEntity = wsmcbl.src.model.secretary.StudentEntity;

namespace wsmcbl.tests.unit.database;

public class DaoFactoryPostgresTest
{
    private readonly DaoFactoryPostgres daoFactory;
    private readonly PostgresContext context;

    public DaoFactoryPostgresTest()
    {
        context = Substitute
            .For<PostgresContext>(new DbContextOptions<PostgresContext>());
        daoFactory = new DaoFactoryPostgres(context);
    }

    [Fact]
    public void getAccountingStudentDao_ReturnsDao()
    {
        var result = daoFactory.studentDao<StudentEntity>();

        Assert.NotNull(result);
        Assert.IsType<StudentDaoPostgres>(result);
    }
    
    [Fact]
    public void getSecretaryStudentDao_ReturnsDao()
    {
        var result = daoFactory.studentDao<SecretaryStudentEntity>();

        Assert.NotNull(result);
        Assert.IsType<SecretaryStudentDaoPostgres>(result);
    }


    [Fact]
    public void getTariffDao_ReturnsDao()
    {
        getDao_ReturnsDao(() => daoFactory.tariffDao);
    }


    [Fact]
    public void getCashierDao_ReturnsDao()
    {
        getDao_ReturnsDao(() => daoFactory.cashierDao);
    }


    [Fact]
    public void getTransactionDao_ReturnsDao()
    {
        getDao_ReturnsDao(() => daoFactory.transactionDao);
    }


    [Fact]
    public void getTariffTypeDao_ReturnsDao()
    {
        getDao_ReturnsDao(() => daoFactory.tariffTypeDao);
    }


    [Fact]
    public void getDebtHistoryDao_ReturnsDao()
    {
        getDao_ReturnsDao(() => daoFactory.debtHistoryDao);
    }

    
    private static void getDao_ReturnsDao<TDao>(Func<TDao> getDao) where TDao : class
    {
        var result = getDao();
        
        Assert.NotNull(result);
        Assert.IsAssignableFrom<TDao>(result);
    }
    
    [Fact]
    public async Task execute_CallDbContext()
    {
        await daoFactory.execute();
        
        await context.Received().SaveChangesAsync();
    }
}