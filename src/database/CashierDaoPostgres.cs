using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.config;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.database;

public class CashierDaoPostgres(PostgresContext context) : GenericDaoPostgres<CashierEntity, string>(context), ICashierDao
{
    public override async Task<CashierEntity?> getById(string id)
    {
        var cashier = await entities.Where(e => e.cashierId == id)
            .Include(e => e.user).FirstOrDefaultAsync();
        if (cashier == null)
        {
            throw new EntityNotFoundException("Cashier", id);
        }
        
        return cashier;
    }
}