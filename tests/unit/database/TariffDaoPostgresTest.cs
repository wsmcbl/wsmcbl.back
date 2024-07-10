using wsmcbl.src.database;
using wsmcbl.src.model.accounting;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.database;

public class TariffDaoPostgresTest : BaseDaoPostgresTest
{
    private TariffDaoPostgres? dao;

    [Fact]
    public async Task getOverdueList_ReturnsList()
    {
        var entityGenerator = new TestEntityGenerator();
        var list = entityGenerator.aTariffList();

        var tariffEntities = TestDbSet<TariffEntity>.getFake(list);
        context.Set<TariffEntity>().Returns(tariffEntities);

        dao = new TariffDaoPostgres(context);
        
        var result = await dao.getOverdueList();
        
        Assert.NotEmpty(result);
        Assert.Equal(list, result);
    }
    
    [Fact]
    public async Task getOverdueList_EmptyList()
    {
        List<TariffEntity> list = [];

        var tariffEntities = TestDbSet<TariffEntity>.getFake(list);
        context.Set<TariffEntity>().Returns(tariffEntities);

        dao = new TariffDaoPostgres(context);
        
        var result = await dao.getOverdueList();
        
        Assert.Empty(result);
    }


    
    [Fact]
    public async Task getListByStudent_ReturnsList()
    {
        var entityGenerator = new TestEntityGenerator();
        var tariffList = entityGenerator.aTariffList();
        var debtList = entityGenerator.aDebtHistoryList("std-1");

        context = TestDbContext.getInMemory("tariffdao");
        context.Set<TariffEntity>().AddRange(tariffList);
        context.Set<DebtHistoryEntity>().AddRange(debtList);
        await context.SaveChangesAsync();

        dao = new TariffDaoPostgres(context);

        var result = await dao.getListByStudent("std-1");
        
        Assert.NotEmpty(result);
        Assert.Equal(tariffList, result);
    }
    
    [Fact]
    public async Task getListByStudent_EmptyList()
    {
        context = TestDbContext.getInMemory("tariffdao");

        dao = new TariffDaoPostgres(context);

        var result = await dao.getListByStudent("std-1");
        
        Assert.Empty(result);
    }



    [Fact]
    public async Task getGeneralBalance_ReturnsFloatArray()
    {
        var entityGenerator = new TestEntityGenerator();
        var debtList = entityGenerator.aDebtHistoryList("std-1");

        context = TestDbContext.getInMemory("tariffdao");
        context.Set<DebtHistoryEntity>().AddRange(debtList);
        await context.SaveChangesAsync();

        dao = new TariffDaoPostgres(context);
        
        var result = await dao.getGeneralBalance("std-1");
        
        Assert.IsType<float[]>(result);
        Assert.Equal([0,1000], result);
    }
    
    [Fact]
    public async Task getGeneralBalance_EmptyDebList_ReturnsFloatArray()
    {
        context = TestDbContext.getInMemory("tariffdao");

        dao = new TariffDaoPostgres(context);
        
        var result = await dao.getGeneralBalance("std-1");
        
        Assert.IsType<float[]>(result);
        Assert.Equal([0,0], result);

    }
}