using wsmcbl.src.model.accounting;

namespace wsmcbl.src.controller.business;

public interface ICollectTariffController
{
    public Task<StudentEntity> getStudent(string studentId);
    public Task<List<StudentEntity>> getStudentsList();
    
    public Task<List<TariffEntity>> getTariffListByStudent(string studentId);
    public Task<List<TariffEntity>> getOverdueTariffList();
    
    public Task applyArrears(int tariffId);
    
    public Task<string> saveTransaction(TransactionEntity transaction);
    public Task<(TransactionEntity, StudentEntity, CashierEntity, float[])> getFullTransaction(string transactionId);
    public Task<List<TariffTypeEntity>> getTariffTypeList();
    
    public Task exonerateArrears(string studentId, List<DebtHistoryEntity> debts);
}