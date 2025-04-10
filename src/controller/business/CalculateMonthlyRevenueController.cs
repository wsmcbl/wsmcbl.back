using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class CalculateMonthlyRevenueController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<object?> getExpectedMonthly()
    {
        return await daoFactory.debtHistoryDao!.getAll();
    }

    public async Task<object?> getExpectedMonthlyReceived()
    {
        return await daoFactory.debtHistoryDao!.getAll();
    }

    public async Task<object?> getTotalReceived()
    {
        return await daoFactory.transactionDao!.getAll();
    }
}