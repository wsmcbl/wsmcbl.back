using wsmcbl.back.model.config;
using wsmcbl.back.model.dao;

namespace wsmcbl.back.model.accounting;

public interface ICashierDao : IGenericDao<CashierEntity, string>;

public interface IStudentDao : IGenericDao<StudentEntity, string>;

public interface IUserDao : IGenericDao<UserEntity, string>;

public interface ITransactionDao : IGenericDao<TransactionEntity, string>;

public interface ITariffDao : IGenericDao<TariffEntity, int>
{
    public Task<List<TariffEntity>> getOverdueList();
    public Task<List<TariffEntity>> getListByStudent(string studentId);
    public Task<float[]> getGeneralBalance(string studentId);
}
