using wsmcbl.src.model.accounting;

namespace wsmcbl.src.controller.business;

public interface ICollectTariffController
{
    public Task<StudentEntity> getStudentById(string studentId);
    public Task<List<StudentEntity>> getStudentsList();
    
    public Task<List<TariffEntity>> getTariffListByStudent(string studentId);
    public Task<List<TariffEntity>> getOverdueTariffList();
    
    public Task<TariffEntity> applyArrears(int tariffId);
    
    public Task<string> saveTransaction(TransactionEntity transaction, List<DebtHistoryEntity> debtList);
    public Task<List<TariffTypeEntity>> getTariffTypeList();

    public Task<byte[]> getInvoiceDocument(string transactionId);
}