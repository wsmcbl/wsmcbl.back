using wsmcbl.src.database;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.secretary;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.database;

public class TariffDaoPostgresTest : BaseDaoPostgresTest
{
    private TariffDaoPostgres? sut;

    [Fact]
    public async Task createRange_ShouldCreateList_WhenCalled()
    {
        var tariff = TestEntityGenerator.aTariff();
        context = TestDbContext.getInMemory();
        
        sut = new TariffDaoPostgres(context);
        
        await sut.createRange([tariff]);
        
        await context.SaveChangesAsync();
        Assert.Equal(tariff, context.Set<TariffEntity>().First(e => e.tariffId == tariff.tariffId));
    }
    

    [Fact]
    public async Task getOverdueList_ReturnsList()
    {
        var list = TestEntityGenerator.aTariffList();

        context = TestDbContext.getInMemory();
        context.Set<TariffEntity>().AddRange(list);
        context.Set<SchoolyearEntity>().AddRange(TestEntityGenerator.aSchoolYearList());
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
        context.Set<SchoolyearEntity>().AddRange(TestEntityGenerator.aSchoolYearList());
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
        context.Set<SchoolyearEntity>().AddRange(TestEntityGenerator.aSchoolYearList());
        await context.SaveChangesAsync();

        sut = new TariffDaoPostgres(context);

        var result = await sut.getListByStudent("std-1");

        Assert.NotEmpty(result);
        Assert.True(result.Count > 1);
    }
    
    [Fact]
    public async Task getListByStudent_EmptyList()
    {
        context = TestDbContext.getInMemory();
        context.Set<SchoolyearEntity>().AddRange(TestEntityGenerator.aSchoolYearList());
        await context.SaveChangesAsync();

        sut = new TariffDaoPostgres(context);

        var result = await sut.getListByStudent("std-1");
        
        Assert.Empty(result);
    }
}