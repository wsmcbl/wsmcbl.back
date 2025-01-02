using Microsoft.EntityFrameworkCore;
using NSubstitute;
using wsmcbl.src.database;
using wsmcbl.src.model.accounting;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.database;

public class DebtHistoryDaoPostgresTest : BaseDaoPostgresTest
{
    private DebtHistoryDaoPostgres? sut;
    
    [Fact]
    public async Task haveTariffsAlreadyPaid_ShouldReturnTrue_WhenDebtIsPaid()
    {
        var debt = TestEntityGenerator.aDebtHistory("std-1", true);
        var transaction = TestEntityGenerator.aTransaction("std-1", [TestEntityGenerator.aTransactionTariff()]);

        context = TestDbContext.getInMemory();
        context.Set<DebtHistoryEntity>().Add(debt);
        await context.SaveChangesAsync();
        
        sut = new DebtHistoryDaoPostgres(context);

        var result = await sut.haveTariffsAlreadyPaid(transaction);

        Assert.True(result);
    }

    [Fact]
    public async Task getListByStudentWithPayments_ReturnsList()
    {
        var list = TestEntityGenerator.aDebtHistoryList("std-1", false);

        var debtEntities = TestDbSet<DebtHistoryEntity>.getFake(list);
        context.Set<DebtHistoryEntity>().Returns(debtEntities);
        sut = new DebtHistoryDaoPostgres(context);

        var result = await sut.getListByStudentWithPayments("std-1");

        Assert.NotEmpty(result);
        Assert.Equal(list, result);
    }

    [Fact]
    public async Task getListByStudent_EmptyList()
    {
        var debtEntities = TestDbSet<DebtHistoryEntity>.getFake([]);
        context.Set<DebtHistoryEntity>().Returns(debtEntities);
        sut = new DebtHistoryDaoPostgres(context);

        var result = await sut.getListByStudentWithPayments("std-1");

        Assert.Empty(result);
    }

    [Fact]
    public async Task exonerateArrears_ShouldNoExonerate_WhenListIsEmpty()
    {
        context = TestDbContext.getInMemory();
        
        sut = new DebtHistoryDaoPostgres(context);
        await sut.exonerateArrears("std-1", []);

        await context.SaveChangesAsync();
        var result = await context.Set<DebtHistoryEntity>().AnyAsync();
        Assert.False(result);
    }

    [Fact]
    public async Task exonerateArrears_ArrearsExonerate()
    {
        var list = TestEntityGenerator.aDebtHistoryList("std-1", false);

        context = TestDbContext.getInMemory();
        context.Set<DebtHistoryEntity>().AddRange(list);
        await context.SaveChangesAsync();

        sut = new DebtHistoryDaoPostgres(context);

        await sut.exonerateArrears("std-1", list);

        var debt = await context.Set<DebtHistoryEntity>().FirstOrDefaultAsync(t => t.studentId == "std-1");

        Assert.NotNull(debt);
        Assert.Equal(list[0], debt);
        Assert.Equal(0, debt.arrears);
    }
}