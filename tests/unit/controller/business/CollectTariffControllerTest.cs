using wsmcbl.src.controller.business;
using wsmcbl.src.dto.output;
using wsmcbl.src.exception;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.config;
using wsmcbl.src.model.dao;

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
        var list = EntityMaker.getAStudentList();
        studentDao.getAll().Returns(list);
        daoFactory.studentDao<StudentEntity>().Returns(studentDao);

        var result = await controller.getStudentsList();

        Assert.IsType<List<StudentEntity>>(result);
        Assert.NotEmpty(result);
        Assert.Equal(list, result);
    }

    [Fact]
    public async Task getStudentsList_EmptyList()
    {
        studentDao.getAll().Returns([]);
        daoFactory.studentDao<StudentEntity>().Returns(studentDao);

        var result = await controller.getStudentsList();

        Assert.IsType<List<StudentEntity>>(result);
        Assert.Empty(result);
    }

    
    [Fact]
    public async Task getStudent_ReturnsStudent()
    {
        const string studentId = "std";
        studentDao.getById(studentId).Returns(EntityMaker.getAStudent(studentId));
        daoFactory.studentDao<StudentEntity>().Returns(studentDao);

        var result = await controller.getStudent(studentId);

        Assert.IsType<StudentEntity>(result);
        Assert.NotNull(result);
        Assert.Equal(studentId, result.studentId);
    }
    
    [Fact]
    public async Task getStudent_StudentNotFound_ReturnsException()
    {
        const string studentId = "std";
        studentDao.getById(studentId).Returns(Task.FromResult<StudentEntity?>(null));
        daoFactory.studentDao<StudentEntity>().Returns(studentDao);

        await Assert.ThrowsAsync<EntityNotFoundException>(() => controller.getStudent(studentId));
    }

    
    [Fact]
    public async Task getTariffListByStudent_ReturnsList()
    {
        const string studentId = "std";
        var list = EntityMaker.getATariffList();
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
        var list = EntityMaker.getATariffList();
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
        var tariff = new TariffEntity{ tariffId = tariffId, isLate = false };
        
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
        var tariff = new TariffEntity{ tariffId = tariffId, isLate = true };
        
        tariffDao.getById(tariffId).Returns(tariff);
        daoFactory.tariffDao.Returns(tariffDao);
        
        await Assert.ThrowsAsync<EntityUpdateException>(() => controller.applyArrears(tariffId));
    }
    
    
    [Fact]
    public async Task saveTransaction_TransactionIsSave()
    {
        var entity = new TransactionEntity { transactionId = "tst" };
        var transactionDao = Substitute.For<ITransactionDao>();
        daoFactory.transactionDao.Returns(transactionDao);

        var result = await controller.saveTransaction(entity);

        await transactionDao.Received().create(entity);
        Assert.Equal(entity.transactionId, result);
    }


    [Fact]
    public async Task getFullTransaction_ReturnsInvoice()
    {
        var initTransaction = Substitute.For<TransactionEntity>();
        initTransaction.studentId = "std";
        initTransaction.transactionId = "std";
        initTransaction.cashierId = "csh";
        
        var transactionDao = Substitute.For<ITransactionDao>();
        var cashierDao = Substitute.For<ICashierDao>();
        
        var initStudent = EntityMaker.getAStudent("std");
        var initCashier = new CashierEntity{cashierId = "csh", user = new UserEntity()};

        cashierDao.getById("csh").Returns(initCashier);
        tariffDao.getGeneralBalance("std").Returns([1, 2]);
        transactionDao.getById(initTransaction.transactionId).Returns(initTransaction);
        daoFactory.cashierDao.Returns(cashierDao);
        daoFactory.tariffDao.Returns(tariffDao);
        daoFactory.transactionDao.Returns(transactionDao);
        
        controller.getStudent("std").Returns(initStudent);

        var (transaction, student, cashier, generalBalance) = await controller.getFullTransaction(initTransaction.transactionId);
        
        Assert.Equal(initTransaction, transaction);
        Assert.Equal(initStudent, student);
        Assert.Equal(initCashier, cashier);
        Assert.Equal([1, 2], generalBalance);
    }
    
    
    [Fact]
    public async Task getTariffTypeList_ReturnsList()
    {
        var list = EntityMaker.getTariffTypeList();
        var tariffTypeDao = Substitute.For<ITariffTypeDao>();
        tariffTypeDao.getAll().Returns(list);
        daoFactory.tariffTypeDao.Returns(tariffTypeDao);

        var result = await controller.getTariffTypeList();

        Assert.IsType<List<TariffTypeEntity>>(result);
        Assert.NotEmpty(result);
        Assert.Equal(list, result);
    }

    
    [Fact]
    public async Task exonerateArrears_ArrearsAreExonerates()
    {
        List<DebtHistoryEntity> list = [];
        var debtHistoryDao = Substitute.For<IDebtHistoryDao>();
        daoFactory.debtHistoryDao.Returns(debtHistoryDao);

        await controller.exonerateArrears(list);

        await debtHistoryDao.Received().exonerateArrears(list);
    }
}