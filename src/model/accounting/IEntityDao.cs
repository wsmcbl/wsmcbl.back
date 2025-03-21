using wsmcbl.src.model.dao;

namespace wsmcbl.src.model.accounting;

public interface ICashierDao : IGenericDao<CashierEntity, string>
{
    public Task<CashierEntity> getByUserId(Guid userId);
}

public interface IStudentDao : IGenericDao<StudentEntity, string>
{
    public Task<StudentEntity> getFullById(string studentId);
    public Task<PagedResult<StudentView>> getStudentViewList(PagedRequest request);
    public Task<List<StudentEntity>> getAllWithSolvencyInRegistration();

    public Task<List<DebtorStudentView>> getDebtorStudentList(); 
    
    public Task<bool> hasSolvencyInRegistration(string studentId);
}

public interface ITransactionDao : IGenericDao<TransactionEntity, string>
{
    public Task<List<TransactionReportView>> getByRange(DateTime from, DateTime to);
    public Task<PagedResult<TransactionReportView>> getAll(TransactionReportViewPagedRequest request);
    public Task<List<TransactionInvoiceView>> getTransactionInvoiceViewList(DateTime from, DateTime to);
}

public interface ITariffTypeDao : IGenericDao<TariffTypeEntity, int>;

public interface IExchangeRateDao : IGenericDao<ExchangeRateEntity, int>
{
    public Task<ExchangeRateEntity> getLastRate();
}

public interface ITariffDao : IGenericDao<TariffEntity, int>
{
    public Task createRange(List<TariffEntity> tariffList);
    public Task<List<TariffEntity>> getOverdueList();
    public Task<List<TariffEntity>> getListByStudent(string studentId);
    public Task<TariffEntity> getRegistrationTariff(string schoolyear, int level);
    public Task<List<TariffEntity>> getCurrentRegistrationTariffList();
}

public interface IDebtHistoryDao : IGenericDao<DebtHistoryEntity, string>
{
    public Task<DebtHistoryEntity> forgiveADebt(string studentId, int tariffId);
    public Task<List<DebtHistoryEntity>> getListByStudentId(string studentId);
    public Task<List<DebtHistoryEntity>> getListByTransactionId(TransactionEntity transaction);
    public Task<PagedResult<DebtHistoryEntity>> getPaginatedByStudentId(string studentId, PagedRequest request);
    
    public Task<decimal[]> getGeneralBalance(string studentId);
    public Task<bool> hasPaidTariffsInTransaction(TransactionEntity transaction);
    
    public Task restoreDebt(string transactionId);
    public Task createRegistrationDebtByStudent(StudentEntity student);
    public Task exonerateArrears(string studentId, List<DebtHistoryEntity> list);
}