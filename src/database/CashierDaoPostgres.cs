using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.accounting;

namespace wsmcbl.src.database;

public class CashierDaoPostgres(PostgresContext context) : GenericDaoPostgres<CashierEntity, string>(context), ICashierDao
{
    public override async Task<CashierEntity?> getById(string id)
    {
        var cashier = await entities.Include(e => e.user)
            .FirstOrDefaultAsync(e => e.cashierId == id);
        
        if (cashier == null)
        {
            throw new EntityNotFoundException("CashierEntity", id);
        }
        
        return cashier;
    }

    public async Task<CashierEntity> getByUserId(Guid userId)
    {
        var cashier = await entities.FirstOrDefaultAsync(e => e.userId == userId);
        if (cashier == null)
        {
            throw new EntityNotFoundException($"Cashier with user id ({userId.ToString()}) not found.");
        }

        return cashier;
    }
}