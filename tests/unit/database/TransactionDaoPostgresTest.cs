using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database;
using wsmcbl.src.model.accounting;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.database;

public class TransactionDaoPostgresTest : BaseDaoPostgresTest
{
    [Fact]
    public async Task create_EntityCreate()
    {
        var entityGenerator = new TestEntityGenerator();
        var transaction = entityGenerator.aTransaction("std-1", []);

        var transactionEntities = TestDbSet<TransactionEntity>.getFake([transaction]);
        context.Set<TransactionEntity>().Returns(transactionEntities);

        var dao = new TransactionDaoPostgres(context);
        
        dao.create(transaction);

        var createdTransaction = await context.Set<TransactionEntity>()
            .Where(t => t.transactionId == transaction.transactionId!).ToListAsync();
        
        Assert.Equal(transaction, createdTransaction[0]);
    }
}
