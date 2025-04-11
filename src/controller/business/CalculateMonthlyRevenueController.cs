using wsmcbl.src.model.accounting;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class CalculateMonthlyRevenueController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<List<DebtHistoryEntity>> getExpectedMonthly(DateTime startDate, bool paid = false)
    {
        return await daoFactory.debtHistoryDao!.getAllByMonth(startDate, paid);
    }

    public async Task<List<TransactionTariffView>> getTotalReceived(DateTime startDate)
    {
        return await daoFactory.transactionDao!.getTransactionTariffViewListByDate(startDate);
    }
}