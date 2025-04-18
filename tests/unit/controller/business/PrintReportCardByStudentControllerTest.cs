using NSubstitute;
using wsmcbl.src.controller.business;
using wsmcbl.src.exception;
using wsmcbl.src.model.dao;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.controller.business;

public class PrintDocumentByStudentControllerTest
{
    private PrintDocumentByStudentController sut;
    private readonly DaoFactory daoFactory;

    public PrintDocumentByStudentControllerTest()
    {
        daoFactory = Substitute.For<DaoFactory>();
        sut = new PrintDocumentByStudentController(daoFactory);
    }

    [Fact]
    public async Task getStudentSolvency_ShouldReturnTrue_WhenStudentHasSolvency()
    {
        const string studentId = "2024-0001-hola";
        var debtHistoryList = TestEntityGenerator.aDebtHistoryList(studentId);
        debtHistoryList[0].tariff.dueDate = DateOnly.FromDateTime(DateTime.Today);
        
        daoFactory.debtHistoryDao!.getListByStudentId(studentId).Returns(debtHistoryList);
        
        var result = await sut.isStudentSolvent(studentId);
        
        Assert.True(result);
    }
    
    [Fact]
    public async Task getStudentSolvency_ShouldReturnFalse_WhenStudentHasNoSolvency()
    {
        const string studentId = "2024-0001-hola";
        var debtHistoryList = TestEntityGenerator.aDebtHistoryList(studentId);
        debtHistoryList[0].tariff.dueDate = DateOnly.FromDateTime(DateTime.Today);
        debtHistoryList[0].isPaid = false;
        
        daoFactory.debtHistoryDao!.getListByStudentId(studentId).Returns(debtHistoryList);
        
        var result = await sut.isStudentSolvent(studentId);
        
        Assert.False(result);
    }
    
    [Fact]
    public async Task getStudentSolvency_ShouldThrowException_WhenStudentHasNotDebtHistory()
    {
        const string studentId = "2024-0001-hola";
        daoFactory.debtHistoryDao!.getListByStudentId(studentId).Returns([]);
        await Assert.ThrowsAsync<EntityNotFoundException>(() => sut.isStudentSolvent(studentId));
    }
}