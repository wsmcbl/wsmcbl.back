using wsmcbl.src.database.context;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;
using IStudentDao = wsmcbl.src.model.accounting.IStudentDao;
using ISubjectDao = wsmcbl.src.model.secretary.ISubjectDao;
using StudentEntity = wsmcbl.src.model.accounting.StudentEntity;

namespace wsmcbl.src.database;

public class DaoFactoryPostgres(PostgresContext context) : DaoFactory
{
    public override async Task execute()
    {
        await context.SaveChangesAsync();
    }
    
    
    private IStudentDao? _accountingStudentDao; 
    public override IStudentDao studentDao => _accountingStudentDao ??= new StudentDaoPostgres(context);
    
    
    private SecretaryStudentDaoPostgres? _secretaryStudentDao;
    public override model.secretary.IStudentDao secretaryStudentDao => _secretaryStudentDao ??= new SecretaryStudentDaoPostgres(context);
    
    
    private ICashierDao? _cashierDao;
    public override ICashierDao cashierDao => _cashierDao ??= new CashierDaoPostgres(context);

    
    private ITariffDao? _tariffDao;
    public override ITariffDao tariffDao => _tariffDao ??= new TariffDaoPostgres(context);

    
    private ITransactionDao? _transactionDao;
    public override ITransactionDao transactionDao => _transactionDao ??= new TransactionDaoPostgres(context);
    
    
    private ITariffTypeDao? _tariffTypeDao;
    public override ITariffTypeDao tariffTypeDao => _tariffTypeDao ??= new TariffTypeDaoPostgres(context);
    
    
    private IDebtHistoryDao? _debtHistoryDao;
    public override IDebtHistoryDao debtHistoryDao => _debtHistoryDao ??= new DebtHistoryDaoPostgres(context);


    private IGradeDao? _gradeDao;
    public override IGradeDao gradeDao => _gradeDao ??= new GradeDaoPostgres(context);

    
    private IEnrollmentDao? _enrollmentDao;
    public override IEnrollmentDao enrollmentDao => _enrollmentDao ??= new EnrollmentDaoPostgres(context);


    private ITeacherDao? _teacherDao;
    public override ITeacherDao teacherDao => _teacherDao ??= new TeacherDaoPostgres(context);
    
    
    private ISubjectDao? _subjectDao;
    public override ISubjectDao subjectDao => _subjectDao ??= new SubjectDaoPostgres(context);
    
    
    private model.academy.ISubjectDao? _academySubjectDao;
    public override model.academy.ISubjectDao academySubjectDao => _academySubjectDao ??= new AcademySubjectDaoPostgres(context);


    private ISchoolyearDao? _schoolyearDao;
    public override ISchoolyearDao? schoolyearDao => _schoolyearDao ??= new SchoolyearDaoPostgres(context);
}