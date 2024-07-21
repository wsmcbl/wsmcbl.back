using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database;
using wsmcbl.src.model.accounting;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.database;

public class DebtHistoryDaoPostgresTest : BaseDaoPostgresTest
{
    private DebtHistoryDaoPostgres? dao;


    [Fact]
    public async Task getListByStudent_ReturnsList()
    {
        var entityGenerator = new TestEntityGenerator();
        var list = entityGenerator.aDebtHistoryList("std-1");

        var debtEntities = TestDbSet<DebtHistoryEntity>.getFake(list);
        context.Set<DebtHistoryEntity>().Returns(debtEntities);
        dao = new DebtHistoryDaoPostgres(context);
        
        var result = await dao.getListByStudent("std-1");
        
        Assert.NotEmpty(result);
        Assert.Equal(list, result);
    }
    
    [Fact]
    public async Task getListByStudent_EmptyList()
    {
        var debtEntities = TestDbSet<DebtHistoryEntity>.getFake([]);
        context.Set<DebtHistoryEntity>().Returns(debtEntities);
        dao = new DebtHistoryDaoPostgres(context);
        
        var result = await dao.getListByStudent("std-1");
        
        Assert.Empty(result);
    }


    [Fact]
    public async Task exonerateArrears_ArrearsExonerate()
    {
        var entityGenerator = new TestEntityGenerator();
        var list = entityGenerator.aDebtHistoryList("std-1");

        context = TestDbContext.getInMemory();
        context.Set<DebtHistoryEntity>().AddRange(list);
        await context.SaveChangesAsync();

        dao = new DebtHistoryDaoPostgres(context);
        
        await dao.exonerateArrears("std-1", list);

        var debt = await context.Set<DebtHistoryEntity>().FirstOrDefaultAsync(t => t.studentId == "std-1");
        
        Assert.NotNull(debt);
        Assert.Equal(list[0], debt);
        Assert.Equal(0,debt.arrear);
    }
}