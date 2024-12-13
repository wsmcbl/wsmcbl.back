using wsmcbl.src.database;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.secretary;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.database;

public class TariffDaoPostgresTest : BaseDaoPostgresTest
{
    private TariffDaoPostgres? sut;

    [Fact]
    public async Task createList_ShouldCreateList_WhenCalled()
    {
        var tariff = TestEntityGenerator.aTariff();
        context = TestDbContext.getInMemory();
        
        sut = new TariffDaoPostgres(context);
        
        sut.createList([tariff]);
        
        await context.SaveChangesAsync();
        Assert.Equal(tariff, context.Set<TariffEntity>().First(e => e.tariffId == tariff.tariffId));
    }
    

    [Fact]
    public async Task getOverdueList_ReturnsList()
    {
        var list = TestEntityGenerator.aTariffList();

        context = TestDbContext.getInMemory();
        context.Set<TariffEntity>().AddRange(list);
        context.Set<SchoolYearEntity>().AddRange(TestEntityGenerator.aSchoolYearList());
        await context.SaveChangesAsync();

        sut = new TariffDaoPostgres(context);
        
        var result = await sut.getOverdueList();
        
        Assert.NotEmpty(result);
        Assert.Equal(list, result);
    }
    
    [Fact]
    public async Task getOverdueList_EmptyList()
    {
        context = TestDbContext.getInMemory();
        context.Set<SchoolYearEntity>().AddRange(TestEntityGenerator.aSchoolYearList());
        await context.SaveChangesAsync();

        sut = new TariffDaoPostgres(context);
        
        var result = await sut.getOverdueList();
        
        Assert.Empty(result);
    }

    
    [Fact]
    public async Task getListByStudent_ShouldReturnsList()
    {
        var debtList = TestEntityGenerator.aDebtHistoryList("std-1", false);

        context = TestDbContext.getInMemory();
        context.Set<DebtHistoryEntity>().AddRange(debtList);
        context.Set<SchoolYearEntity>().AddRange(TestEntityGenerator.aSchoolYearList());
        await context.SaveChangesAsync();

        sut = new TariffDaoPostgres(context);

        var result = await sut.getListByStudent("std-1");

        Assert.NotEmpty(result);
        var tariffList = new List<TariffEntity>
        {
            TestEntityGenerator.aTariff(),
            TestEntityGenerator.aTariffNotMonthly()
        };
        foreach (var item in tariffList)
        {
            Assert.True(result.Contains(item));
        }
    }
    
    [Fact]
    public async Task getListByStudent_EmptyList()
    {
        context = TestDbContext.getInMemory();
        context.Set<SchoolYearEntity>().AddRange(TestEntityGenerator.aSchoolYearList());
        await context.SaveChangesAsync();

        sut = new TariffDaoPostgres(context);

        var result = await sut.getListByStudent("std-1");
        
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

        sut = new TariffDaoPostgres(context);
        
        var result = await sut.getGeneralBalance("std-1");
        
        Assert.IsType<float[]>(result);
        Assert.Equal([10,100], result);
    }
    
    [Fact]
    public async Task getGeneralBalance_EmptyDebList_ReturnsFloatArray()
    {
        context = TestDbContext.getInMemory();
        context.Set<SchoolYearEntity>().AddRange(TestEntityGenerator.aSchoolYearList());
        await context.SaveChangesAsync();

        sut = new TariffDaoPostgres(context);
        
        var result = await sut.getGeneralBalance("std-1");
        
        Assert.IsType<float[]>(result);
        Assert.Equal([0,0], result);
    }
}