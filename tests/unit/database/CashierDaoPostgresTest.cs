using wsmcbl.src.database;
using wsmcbl.src.exception;
using wsmcbl.src.model.accounting;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.database;

public class CashierDaoPostgresTest : BaseDaoPostgresTest
{
    private CashierDaoPostgres? dao;

    [Fact]
    public async Task getById_ReturnsCashier()
    {
        var cashier = TestEntityGenerator.aCashier("csh-1");

        context = TestDbContext.getInMemory();
        await context.Set<CashierEntity>().AddAsync(cashier);
        await context.SaveChangesAsync();
        
        dao = new CashierDaoPostgres(context);
        
        var result = await dao.getById("csh-1");

        Assert.NotNull(result);
        Assert.Equal(cashier, result);
    }


    [Fact]
    public async Task getById_CashierNotFound_ReturnsException()
    {
        context = TestDbContext.getInMemory();
        dao = new CashierDaoPostgres(context);

        await Assert.ThrowsAsync<EntityNotFoundException>(() => dao.getById("csh-1"));
    }
}