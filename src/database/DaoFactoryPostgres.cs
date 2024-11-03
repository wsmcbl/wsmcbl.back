using Microsoft.EntityFrameworkCore;
using wsmcbl.src.database.context;
using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;
using IStudentDao = wsmcbl.src.model.secretary.IStudentDao;

namespace wsmcbl.src.database;

public class DaoFactoryPostgres(PostgresContext context) : DaoFactory
{
    public override async Task execute()
    {
        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new ForbiddenException("Failed to perform transaction. Error: " + e.Message);
        }
    }

    public override void Detached<T>(T element)
    {
        context.Entry(element).State = EntityState.Detached;
    }

    private IPartialDao? _partialDao;
    public override IPartialDao partialDao => _partialDao ??= new PartialDaoPostgres(context);

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


    private IDegreeDao? _degreeDao;
    public override IDegreeDao degreeDao => _degreeDao ??= new DegreeDaoPostgres(context);


    private IEnrollmentDao? _enrollmentDao;
    public override IEnrollmentDao enrollmentDao => _enrollmentDao ??= new EnrollmentDaoPostgres(context);


    private ITeacherDao? _teacherDao;
    public override ITeacherDao teacherDao => _teacherDao ??= new TeacherDaoPostgres(context);


    private ISchoolyearDao? _schoolyearDao;
    public override ISchoolyearDao schoolyearDao => _schoolyearDao ??= new SchoolyearDaoPostgres(context);


    private IDegreeDataDao? _degreeDataDao;
    public override IDegreeDataDao degreeDataDao => _degreeDataDao ??= new DegreeDataDaoPostgres(context);


    private ISubjectDataDao? _subjectDataDao;
    public override ISubjectDataDao subjectDataDao => _subjectDataDao ??= new SubjectDataDaoPostgres(context);


    private ITariffDataDao? _tariffDataDao;
    public override ITariffDataDao tariffDataDao => _tariffDataDao ??= new TariffDataDaoPostgres(context);


    private IStudentDao? _studentDao;
    public override IStudentDao studentDao => _studentDao ??= new StudentDaoPostgres(context);

    private model.academy.IStudentDao? _academyStudentDao;

    public override model.academy.IStudentDao academyStudentDao =>
        _academyStudentDao ??= new AcademyStudentDaoPostgres(context);

    private model.accounting.IStudentDao? _accountingStudentDao;

    public override model.accounting.IStudentDao accountingStudentDao
        => _accountingStudentDao ??= new AccountingStudentDaoPostgres(context);


    private IStudentFileDao? _studentFileDao;
    public override IStudentFileDao studentFileDao => _studentFileDao ??= new StudentFileDaoPostgres(context);


    private IStudentTutorDao? _studentTutorDao;
    public override IStudentTutorDao? studentTutorDao => _studentTutorDao ??= new StudentTutorDaoPostgres(context);


    private IStudentParentDao? _studentParentDao;
    public override IStudentParentDao studentParentDao => _studentParentDao ??= new StudentParentDaoPostgres(context);


    private IStudentMeasurementsDao? _studentMeasurementsDao;

    public override IStudentMeasurementsDao studentMeasurementsDao
        => _studentMeasurementsDao ??= new StudentMeasurementsDaoPostgres(context);
    
    
    private SubjectDaoPostgres? _subjectDao;
    public override ISubjectDao subjectDao => _subjectDao ??= new SubjectDaoPostgres(context);


    private SubjectPartialDaoPostgres? _subjectPartialDao;
    public override ISubjectPartialDao subjectPartialDao => _subjectPartialDao ??= new SubjectPartialDaoPostgres(context); 


    private SemesterDaoPostgres? _semesterDao;
    public override ISemesterDao semesterDao => _semesterDao ??= new SemesterDaoPostgres(context);


    private GradeDaoPostgres? _gradeDao;
    public override IGradeDao gradeDao => _gradeDao ??= new GradeDaoPostgres(context);
}