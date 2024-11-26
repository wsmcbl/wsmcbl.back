using wsmcbl.src.model.accounting;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class CancelTransactionController(DaoFactory daoFactory) : BaseController(daoFactory), ICancelTransactionController
{
    public async Task<List<TransactionReportView>> getTransactionList()
    {
        return await daoFactory.transactionDao!.getByRange(new DateTime(), new DateTime());
    }
}