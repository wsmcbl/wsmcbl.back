using wsmcbl.src.model.accounting;

namespace wsmcbl.src.controller.business;

public class TransactionReportByDateController : ITransactionReportByDateController
{
    public Task<List<TransactionEntity>> getReportByDate(int range)
    {
        throw new NotImplementedException();
    }
}