using Microsoft.EntityFrameworkCore;
using NSubstitute;
using wsmcbl.src.database;
using wsmcbl.src.database.context;
using wsmcbl.src.model.accounting;
using wsmcbl.tests.utilities;
using SecretaryStudentEntity = wsmcbl.src.model.secretary.StudentEntity;

namespace wsmcbl.tests.unit.database;

public class DaoFactoryPostgresTest
{
    private readonly DaoFactoryPostgres sut;
    private readonly PostgresContext context;

    public DaoFactoryPostgresTest()
    {
        context = Substitute.For<PostgresContext>(new DbContextOptions<PostgresContext>());
        sut = new DaoFactoryPostgres(context);
    }

    [Fact]
    public void getAccountingStudentDao_ReturnsDao()
    {
        var result = sut.accountingStudentDao;

        Assert.NotNull(result);
        Assert.IsType<AccountingStudentDaoPostgres>(result);
    }
    
    [Fact]
    public void getSecretaryStudentDao_ReturnsDao()
    {
        var result = sut.studentDao;

        Assert.NotNull(result);
        Assert.IsType<StudentDaoPostgres>(result);
    }


    [Fact]
    public void getTariffDao_ReturnsDao()
    {
        getDao_ReturnsDao(() => sut.tariffDao);
    }


    [Fact]
    public void getCashierDao_ReturnsDao()
    {
        getDao_ReturnsDao(() => sut.cashierDao);
    }


    [Fact]
    public void getTransactionDao_ReturnsDao()
    {
        getDao_ReturnsDao(() => sut.transactionDao);
    }


    [Fact]
    public void getTariffTypeDao_ReturnsDao()
    {
        getDao_ReturnsDao(() => sut.tariffTypeDao);
    }


    [Fact]
    public void getDebtHistoryDao_ReturnsDao()
    {
        getDao_ReturnsDao(() => sut.debtHistoryDao);
    }

    
    private static void getDao_ReturnsDao<TDao>(Func<TDao> getDao) where TDao : class
    {
        var result = getDao();
        
        Assert.NotNull(result);
        Assert.IsAssignableFrom<TDao>(result);
    }
    
    [Fact]
    public async Task execute_CallDbContext()
    {
        await sut.execute();
        
        await context.Received().SaveChangesAsync();
    }
    
    
    
    [Fact]
    public void gradeDao_ShouldReturnGradeDao_WhenCalled()
    {
        getDao_ReturnsDao(() => sut.gradeDao);
    }
    
    [Fact]
    public void enrollmentDao_ShouldReturnEnrollmentDao_WhenCalled()
    {
        getDao_ReturnsDao(() => sut.enrollmentDao);
    }
    
    [Fact]
    public void teacherDao_ShouldReturnTeacherDao_WhenCalled()
    {
        getDao_ReturnsDao(() => sut.teacherDao);
    }
    
    [Fact]
    public void schoolYearDao_ShouldReturnSchoolYearDao_WhenCalled()
    {
        getDao_ReturnsDao(() => sut.schoolyearDao);
    }
    
    [Fact]
    public void gradeDataDao_ShouldReturnIGradeDataDao_WhenCalled()
    {
        getDao_ReturnsDao(() => sut.gradeDataDao);
    }

    [Fact]
    public void subjectDataDao_ShouldReturnISubjectDataDao_WhenCalled()
    {
        getDao_ReturnsDao(() => sut.subjectDataDao);
    }
    
    [Fact]
    public void tariffDataDao_ShouldReturnITariffDataDao_WhenCalled()
    {
        getDao_ReturnsDao(() => sut.tariffDataDao);
    }
}