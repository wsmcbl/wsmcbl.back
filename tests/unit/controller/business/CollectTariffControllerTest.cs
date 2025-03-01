using NSubstitute;
using wsmcbl.src.controller.business;
using wsmcbl.src.exception;
using wsmcbl.src.model;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.dao;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.controller.business;

public class CollectTariffControllerTest
{
    private readonly CollectTariffController sut;
    private readonly DaoFactory daoFactory;
    private readonly IStudentDao studentDao;
    private readonly ITariffDao tariffDao;

    public CollectTariffControllerTest()
    {
        studentDao = Substitute.For<IStudentDao>();
        tariffDao = Substitute.For<ITariffDao>();
        
        daoFactory = Substitute.For<DaoFactory>();
        sut = new CollectTariffController(daoFactory);
    }
    
    [Fact]
    public async Task getStudent_ReturnsStudent()
    {
        const string studentId = "std";
        studentDao.getById(studentId).Returns(TestEntityGenerator.aAccountingStudent(studentId));
        daoFactory.accountingStudentDao.Returns(studentDao);

        var result = await sut.getStudentById(studentId);

        Assert.IsType<StudentEntity>(result);
        Assert.NotNull(result);
        Assert.Equal(studentId, result.studentId);
    }
    
    [Fact]
    public async Task getStudent_StudentNotFound_ReturnsException()
    {
        const string studentId = "std";
        studentDao.getById(studentId).Returns(Task.FromResult<StudentEntity?>(null));
        daoFactory.accountingStudentDao.Returns(studentDao);

        await Assert.ThrowsAsync<EntityNotFoundException>(() => sut.getStudentById(studentId));
    }

    
    [Fact]
    public async Task getTariffListByStudent_ReturnsList()
    {
        const string studentId = "std";
        var list = TestEntityGenerator.aTariffList();
        tariffDao.getListByStudent(studentId).Returns(list);
        daoFactory.tariffDao.Returns(tariffDao);

        var result = await sut.getTariffListByStudent(studentId);

        Assert.IsType<List<TariffEntity>>(result);
        Assert.NotEmpty(result);
        Assert.Equal(list, result);
    }

    [Fact]
    public async Task saveTransaction_TransactionIsSave()
    {
        var entity = new TransactionEntity { transactionId = "tst" };
        var transactionDao = Substitute.For<ITransactionDao>();
        daoFactory.transactionDao.Returns(transactionDao);

        var result = await sut.saveTransaction(entity, []);

        transactionDao.Received().create(entity);
        Assert.Equal(entity, result);
    }
}