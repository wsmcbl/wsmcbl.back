using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database;
using wsmcbl.src.exception;
using wsmcbl.src.model.accounting;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.database;

public class TransactionDaoPostgresTest : BaseDaoPostgresTest
{
    [Fact]
    public void create_ShouldThrowException_WhenDataTransactionIsInvalid()
    {
        var transaction = TestEntityGenerator.aTransaction("", []);
        context = TestDbContext.getInMemory();
        
        var sut = new TransactionDaoPostgres(context);

        Assert.Throws<IncorrectDataException>(() => sut.create(transaction));
    }
    
    [Fact]
    public async Task create_EntityCreate()
    {
        var transaction = TestEntityGenerator.aTransaction("std-1", [new TransactionTariffEntity()]);

        var transactionEntities = TestDbSet<TransactionEntity>.getFake([transaction]);
        context.Set<TransactionEntity>().Returns(transactionEntities);

        var dao = new TransactionDaoPostgres(context);
        
        dao.create(transaction);

        var createdTransaction = await context.Set<TransactionEntity>()
            .Where(t => t.transactionId == transaction.transactionId!).ToListAsync();
        
        Assert.Equal(transaction, createdTransaction[0]);
    }
}
