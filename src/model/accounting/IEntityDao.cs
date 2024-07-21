using wsmcbl.src.model.config;
using wsmcbl.src.model.dao;

namespace wsmcbl.src.model.accounting;

public interface ICashierDao : IGenericDao<CashierEntity, string>;
public interface IStudentDao : IGenericDao<StudentEntity, string>;
public interface IUserDao : IGenericDao<UserEntity, string>;
public interface ITransactionDao : IGenericDao<TransactionEntity, string>;
public interface ITariffTypeDao : IGenericDao<TariffTypeEntity, int>;

public interface ITariffDao : IGenericDao<TariffEntity, int>
{
    public Task<List<TariffEntity>> getOverdueList();
    public Task<List<TariffEntity>> getListByStudent(string studentId);
    public Task<float[]> getGeneralBalance(string studentId);
    public void createList(List<TariffEntity> tariffs);
}

public interface IDebtHistoryDao : IGenericDao<DebtHistoryEntity, string>
{
    public Task<List<DebtHistoryEntity>> getListByStudent(string studentId);
    public Task exonerateArrears(string studentId, List<DebtHistoryEntity> list);
    public Task<bool> haveTariffsAlreadyPaid(TransactionEntity transaction);
}