using wsmcbl.back.model.accounting;

namespace wsmcbl.back.controller.business;

public interface ICollectTariffController
{
    public Task<StudentEntity?> getStudent(string id);
    public Task<List<StudentEntity>> getStudentsList();
    public Task<List<TariffEntity>> getTariffList();
    public Task saveTransaction(TransactionEntity transaction);
    public Task<TransactionEntity?> getLastTransactionByStudent(string studentId);
    
    public Task<CashierEntity?> getCashier(string id);
}