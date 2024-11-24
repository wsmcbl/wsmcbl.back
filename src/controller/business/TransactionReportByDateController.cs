using wsmcbl.src.model.accounting;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class TransactionReportByDateController(DaoFactory daoFactory) : BaseController(daoFactory),
    ITransactionReportByDateController
{
    public async Task<List<TransactionEntity>> getTransactionList(int range)
    {
        //return daoFactory.transactionDao.getByDate(range);
        return await daoFactory.transactionDao!.getAll();
    }
}