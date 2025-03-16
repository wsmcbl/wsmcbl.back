using wsmcbl.src.model.accounting;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.controller.business;

public class ForgetDebtController(DaoFactory daoFactory) : BaseController(daoFactory)
{
    public async Task<DebtHistoryEntity> forgiveADebt(string studentId, int tariffId)
    {
        var result = await daoFactory.debtHistoryDao!.forgiveADebt(studentId, tariffId);
        await daoFactory.ExecuteAsync();
        return result;
    }

    public async Task<List<DebtHistoryEntity>> getDebtListByStudent(string studentId)
    {
        return await daoFactory.debtHistoryDao!.getListByStudentId(studentId);
    }
}