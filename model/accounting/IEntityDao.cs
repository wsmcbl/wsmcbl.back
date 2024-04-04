using wsmcbl.back.model.dao;

namespace wsmcbl.back.model.accounting;

public interface IStudentDao : IGenericDao<StudentEntity, string>;

public interface ITariffDao : IGenericDao<TariffEntity, int>;
public interface ITransactionDao : IGenericDao<TransactionEntity, string>;