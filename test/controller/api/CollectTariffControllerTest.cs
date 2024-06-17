using wsmcbl.src.controller.business;
using wsmcbl.src.dto.output;
using wsmcbl.src.model.accounting;

namespace wsmcbl.test.controller.api;

public class CollectTariffControllerTest : ICollectTariffController
{
    public Task<StudentEntity?> getStudent(string studentId)
    {
        throw new NotImplementedException();
    }

    public Task<List<StudentEntity>> getStudentsList()
    {
        throw new NotImplementedException();
    }

    public Task<List<TariffEntity>> getTariffListByStudent(string studentId)
    {
        throw new NotImplementedException();
    }

    public Task<List<TariffEntity>> getOverdueTariffList()
    {
        throw new NotImplementedException();
    }

    public async Task applyArrears(int tariffId)
    {
        return;
    }

    public Task<string> saveTransaction(TransactionEntity transaction)
    {
        throw new NotImplementedException();
    }

    public Task<InvoiceDto> getFullTransaction(string transactionId)
    {
        throw new NotImplementedException();
    }

    public Task<List<TariffTypeEntity>> getTariffTypeList()
    {
        throw new NotImplementedException();
    }

    public Task exonerateArrears(List<DebtHistoryEntity> debts)
    {
        throw new NotImplementedException();
    }
}