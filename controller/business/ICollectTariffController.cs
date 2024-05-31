using wsmcbl.back.dto.output;
using wsmcbl.back.model.accounting;

namespace wsmcbl.back.controller.business;

public interface ICollectTariffController
{
    public Task<StudentEntity?> getStudent(string studentId);
    public Task<List<StudentEntity>> getStudentsList();
    public Task saveTransaction(TransactionEntity transaction);
    public Task<InvoiceDto> getLastTransactionByStudent(string studentId);
    
    public Task applyArrears(int tariffId);
    public Task<List<TariffEntity>> getTariffList();
    public Task<List<TariffEntity>> getTariffByStudent(string studentId);
    public Task<List<TariffEntity>> getUnexpiredTariff(string schoolyear);
}