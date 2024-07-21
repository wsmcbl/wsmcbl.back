using wsmcbl.src.database;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.secretary;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.database;

public class TariffDaoPostgresTest : BaseDaoPostgresTest
{
    private TariffDaoPostgres? dao;

    [Fact]
    public async Task getOverdueList_ReturnsList()
    {
        var list = TestEntityGenerator.aTariffList();

        context = TestDbContext.getInMemory();
        context.Set<TariffEntity>().AddRange(list);
        context.Set<SchoolYearEntity>().AddRange(TestEntityGenerator.aSchoolYearList());
        await context.SaveChangesAsync();

        dao = new TariffDaoPostgres(context);
        
        var result = await dao.getOverdueList();
        
        Assert.NotEmpty(result);
        Assert.Equal(list, result);
    }
    
    [Fact]
    public async Task getOverdueList_EmptyList()
    {
        context = TestDbContext.getInMemory();
        context.Set<SchoolYearEntity>().AddRange(TestEntityGenerator.aSchoolYearList());
        await context.SaveChangesAsync();

        dao = new TariffDaoPostgres(context);
        
        var result = await dao.getOverdueList();
        
        Assert.Empty(result);
    }


    
    [Fact]
    public async Task getListByStudent_ReturnsList()
    {
        var debtList = TestEntityGenerator.aDebtHistoryList("std-1", false);

        context = TestDbContext.getInMemory();
        context.Set<DebtHistoryEntity>().AddRange(debtList);
        context.Set<SchoolYearEntity>().AddRange(TestEntityGenerator.aSchoolYearList());
        await context.SaveChangesAsync();

        dao = new TariffDaoPostgres(context);

        var result = await dao.getListByStudent("std-1");
        
        Assert.NotEmpty(result);
        Assert.Equivalent(TestEntityGenerator.aTariffList(), result);
    }
    
    [Fact]
    public async Task getListByStudent_EmptyList()
    {
        context = TestDbContext.getInMemory();
        context.Set<SchoolYearEntity>().AddRange(TestEntityGenerator.aSchoolYearList());
        await context.SaveChangesAsync();

        dao = new TariffDaoPostgres(context);

        var result = await dao.getListByStudent("std-1");
        
        Assert.Empty(result);
    }



    [Fact]
    public async Task getGeneralBalance_ReturnsFloatArray()
    {
        var debtList = TestEntityGenerator.aDebtHistoryList("std-1", false);

        context = TestDbContext.getInMemory();
        context.Set<DebtHistoryEntity>().AddRange(debtList);
        context.Set<SchoolYearEntity>().AddRange(TestEntityGenerator.aSchoolYearList());
        await context.SaveChangesAsync();

        dao = new TariffDaoPostgres(context);
        
        var result = await dao.getGeneralBalance("std-1");
        
        Assert.IsType<float[]>(result);
        Assert.Equal([0,1000], result);
    }
    
    [Fact]
    public async Task getGeneralBalance_EmptyDebList_ReturnsFloatArray()
    {
        context = TestDbContext.getInMemory();
        context.Set<SchoolYearEntity>().AddRange(TestEntityGenerator.aSchoolYearList());
        await context.SaveChangesAsync();

        dao = new TariffDaoPostgres(context);
        
        var result = await dao.getGeneralBalance("std-1");
        
        Assert.IsType<float[]>(result);
        Assert.Equal([0,0], result);

    }
}