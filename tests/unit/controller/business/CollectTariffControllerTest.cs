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
    public async Task getStudentsList_ReturnsList()
    {
        var list = TestEntityGenerator.aStudentList();
        studentDao.getStudentViewList().Returns(list);
        daoFactory.accountingStudentDao.Returns(studentDao);

        var result = await sut.getStudentsList();

        Assert.IsType<List<StudentView>>(result);
        Assert.NotEmpty(result);
        Assert.Equal(list, result);
    }

    [Fact]
    public async Task getStudentsList_EmptyList()
    {
        studentDao.getAll().Returns([]);
        daoFactory.accountingStudentDao.Returns(studentDao);

        var result = await sut.getStudentsList();

        Assert.IsType<List<StudentEntity>>(result);
        Assert.Empty(result);
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
    public async Task getOverdueTariffList_ReturnsList()
    {
        var list = TestEntityGenerator.aTariffList();
        tariffDao.getOverdueList().Returns(list);
        daoFactory.tariffDao.Returns(tariffDao);

        var result = await sut.getOverdueTariffList();

        Assert.IsType<List<TariffEntity>>(result);
        Assert.NotEmpty(result);
        Assert.Equal(list, result);
    }


    [Fact]
    public async Task applyArrears_ValidId_ArrearsApplied()
    {
        const int tariffId = 10;
        var tariff = new TariffEntity();
        tariff.tariffId = tariffId;
        tariff.isLate = false; 
        
        tariffDao.getById(tariffId).Returns(tariff);
        daoFactory.tariffDao.Returns(tariffDao);

        await sut.applyArrears(tariffId);
        Assert.True(tariff.isLate);
    }
    
    [Fact]
    public async Task applyArrears_InvalidId_ReturnsException()
    {
        daoFactory.tariffDao.Returns(tariffDao);
        await Assert.ThrowsAsync<EntityNotFoundException>(() => sut.applyArrears(1));
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
    
    [Fact]
    public async Task getTariffTypeList_ReturnsList()
    {
        var list = TestEntityGenerator.aTariffTypeList();
        var tariffTypeDao = Substitute.For<ITariffTypeDao>();
        tariffTypeDao.getAll().Returns(list);
        daoFactory.tariffTypeDao.Returns(tariffTypeDao);

        var result = await sut.getTariffTypeList();

        Assert.IsType<List<TariffTypeEntity>>(result);
        Assert.NotEmpty(result);
        Assert.Equal(list, result);
    }
}