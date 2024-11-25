using wsmcbl.src.model.accounting;
using StudentEntity = wsmcbl.src.model.academy.StudentEntity;

namespace wsmcbl.src.controller.business;

public interface ITransactionReportByDateController
{
    public Task<List<(TransactionEntity, StudentEntity)>> getTransactionList(int range);
    public Task<string> getUserName(string getAuthenticatedUserId);
    public (DateTime start, DateTime end) getDateRange(int range);
    public List<(int quantity, double total)> getSummary();
}