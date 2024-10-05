using NSubstitute;
using wsmcbl.src.controller.business;
using wsmcbl.src.exception;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.config;
using wsmcbl.src.model.dao;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.controller.business;

public class CollectTariffControllerTest
{
    private readonly CollectTariffController controller;
    private readonly DaoFactory daoFactory;
    private readonly IStudentDao studentDao;
    private readonly ITariffDao tariffDao;

    public CollectTariffControllerTest()
    {
        studentDao = Substitute.For<IStudentDao>();
        tariffDao = Substitute.For<ITariffDao>();
        
        daoFactory = Substitute.For<DaoFactory>();
        controller = new CollectTariffController(daoFactory);
    }

    [Fact]
    public async Task getStudentsList_ReturnsList()
    {
        var list = TestEntityGenerator.aStudentList();
        studentDao.getAll().Returns(list);
        daoFactory.accountingStudentDao.Returns(studentDao);

        var result = await controller.getStudentsList();

        Assert.IsType<List<StudentEntity>>(result);
        Assert.NotEmpty(result);
        Assert.Equal(list, result);
    }

    [Fact]
    public async Task getStudentsList_EmptyList()
    {
        studentDao.getAll().Returns([]);
        daoFactory.accountingStudentDao.Returns(studentDao);

        var result = await controller.getStudentsList();

        Assert.IsType<List<StudentEntity>>(result);
        Assert.Empty(result);
    }

    
    [Fact]
    public async Task getStudent_ReturnsStudent()
    {
        const string studentId = "std";
        studentDao.getById(studentId).Returns(TestEntityGenerator.aAccountingStudent(studentId));
        daoFactory.accountingStudentDao.Returns(studentDao);

        var result = await controller.getStudentById(studentId);

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

        await Assert.ThrowsAsync<EntityNotFoundException>(() => controller.getStudentById(studentId));
    }

    
    [Fact]
    public async Task getTariffListByStudent_ReturnsList()
    {
        const string studentId = "std";
        var list = TestEntityGenerator.aTariffList();
        tariffDao.getListByStudent(studentId).Returns(list);
        daoFactory.tariffDao.Returns(tariffDao);

        var result = await controller.getTariffListByStudent(studentId);

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

        var result = await controller.getOverdueTariffList();

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

        await controller.applyArrears(tariffId);
        Assert.True(tariff.isLate);
    }
    
    [Fact]
    public async Task applyArrears_InvalidId_ReturnsException()
    {
        daoFactory.tariffDao.Returns(tariffDao);
        await Assert.ThrowsAsync<EntityNotFoundException>(() => controller.applyArrears(1));
    }
    
    [Fact]
    public async Task applyArrears_TariffAlreadyUpdate_ReturnsException()
    {
        const int tariffId = 10;
        var tariff = new TariffEntity();
        tariff.tariffId = tariffId;
        tariff.isLate = true;
        
        tariffDao.getById(tariffId).Returns(tariff);
        daoFactory.tariffDao.Returns(tariffDao);
        
        await Assert.ThrowsAsync<EntityUpdateConflictException>(() => controller.applyArrears(tariffId));
    }
    
    
    [Fact]
    public async Task saveTransaction_TransactionIsSave()
    {
        var entity = new TransactionEntity { transactionId = "tst" };
        var transactionDao = Substitute.For<ITransactionDao>();
        daoFactory.transactionDao.Returns(transactionDao);

        var result = await controller.saveTransaction(entity, []);

        transactionDao.Received().create(entity);
        Assert.Equal(entity.transactionId, result);
    }


    [Fact]
    public async Task getFullTransaction_ReturnsInvoice()
    {
        var initStudent = TestEntityGenerator.aAccountingStudent("std");
        var initCashier = new CashierEntity{cashierId = "csh", user = new UserEntity()};
        var initTransaction = new TransactionEntity { transactionId = "std", studentId = "std", cashierId = "csh" };
        
        var cashierDao = Substitute.For<ICashierDao>();
        var transactionDao = Substitute.For<ITransactionDao>();

        cashierDao.getById("csh").Returns(initCashier);
        tariffDao.getGeneralBalance("std").Returns([1, 2]);
        transactionDao.getById(initTransaction.transactionId).Returns(initTransaction);
        
        daoFactory.tariffDao.Returns(tariffDao);
        daoFactory.cashierDao.Returns(cashierDao);
        daoFactory.transactionDao.Returns(transactionDao);
        
        controller.getStudentById("std").Returns(initStudent);

        var (transaction, student, cashier, generalBalance) = await controller.getFullTransaction(initTransaction.transactionId);
        
        Assert.Equal(initTransaction, transaction);
        Assert.Equal(initStudent, student);
        Assert.Equal(initCashier, cashier);
        Assert.Equal([1, 2], generalBalance);
    }
    
    [Fact]
    public async Task getFullTransaction_TransactionNull_ReturnsException()
    {
        const string transactionId = "std";
        var transactionDao = Substitute.For<ITransactionDao>();
        transactionDao.getById(transactionId).Returns(Task.FromResult<TransactionEntity?>(null));
        daoFactory.transactionDao.Returns(transactionDao);

        await Assert.ThrowsAsync<EntityNotFoundException>(() => controller.getFullTransaction(transactionId));
    }
    
    
    [Fact]
    public async Task getTariffTypeList_ReturnsList()
    {
        var list = TestEntityGenerator.aTariffTypeList();
        var tariffTypeDao = Substitute.For<ITariffTypeDao>();
        tariffTypeDao.getAll().Returns(list);
        daoFactory.tariffTypeDao.Returns(tariffTypeDao);

        var result = await controller.getTariffTypeList();

        Assert.IsType<List<TariffTypeEntity>>(result);
        Assert.NotEmpty(result);
        Assert.Equal(list, result);
    }
}