using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.accounting;

namespace wsmcbl.src.database;

public class CashierDaoPostgres(PostgresContext context) : GenericDaoPostgres<CashierEntity, string>(context), ICashierDao
{
    public override async Task<CashierEntity?> getById(string id)
    {
        var cashier = await base.getById(id);

        if (cashier is null)
        {
            throw new EntityNotFoundException("Cashier", id);
        }
        
        var service = new UserDaoPostgres(context);
        var user = await service.getById(cashier.userId);

        cashier.user = user!;
        
        return cashier;
    }
}