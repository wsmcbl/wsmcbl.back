using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.api;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.output;
using wsmcbl.src.exception;
using wsmcbl.src.model.accounting;
using DtoMapper = wsmcbl.src.dto.input.DtoMapper;

namespace wsmcbl.tests.unit.controller.api;

public class CollectTariffActionsTest
{
    private readonly ICollectTariffController controller;
    private readonly CollectTariffActions actions;

    public CollectTariffActionsTest()
    {
        controller = Substitute.For<ICollectTariffController>();
        actions = new CollectTariffActions(controller);
    }
    
    private static OkObjectResult assertAndGetOkResult(IActionResult actionResult) =>
        Assert.IsType<OkObjectResult>(actionResult);
    
    
    [Fact]
    public async Task getStudentList_ReturnsList()
    {
        controller.getStudentsList().Returns(EntityMaker.getAStudentList());
        
        var actionResult = await actions.getStudentList();

        var result = assertAndGetOkResult(actionResult);
        var list = Assert.IsType<List<StudentBasicDto>>(result.Value);
        Assert.NotEmpty(list);
        Assert.Equal(2, list.Count);
    }
    
    [Fact]
    public async Task getStudentList_EmptyList()
    {
        var studentList = new List<StudentEntity>();
        controller.getStudentsList().Returns(studentList);
        
        var actionResult = await actions.getStudentList();

        var result = assertAndGetOkResult(actionResult);
        var list = Assert.IsType<List<StudentBasicDto>>(result.Value);
        Assert.Empty(list);
    }
    

    [Fact]
    public async Task getStudentById_ValidId_ReturnStudent()
    {
        const string studentId = "id1";
        controller.getStudent(studentId).Returns(EntityMaker.getAStudent(studentId));

        var actionResult = await actions.getStudentById(studentId);

        var result = assertAndGetOkResult(actionResult);
        var studentDto = Assert.IsType<StudentDto>(result.Value);
        Assert.NotNull(studentDto);
    }

    [Fact]
    public async Task getStudentById_InvalidId_ReturnException()
    {
        await Assert.ThrowsAsync<EntityNotFoundException>(() => actions.getStudentById("id2"));
    }

    [Fact]
    public async Task getStudentById_NullParameter_ReturnException()
    {
        await Assert.ThrowsAsync<EntityNotFoundException>(() => actions.getStudentById(string.Empty));
    }
    

    [Fact]
    public async Task getTariffList_ValidStudentParameter_ReturnsList()
    {
        const string studentId = "id1";
        var initList = EntityMaker.getATariffList();
        controller.getTariffListByStudent(studentId).Returns(initList);

        var actionResult = await actions.getTariffList($"student:{studentId}");

        var result = assertAndGetOkResult(actionResult);
        var list = Assert.IsType<List<TariffEntity>>(result.Value);
        Assert.NotEmpty(list);
        Assert.Equal(initList, list);
    }

    [Fact]
    public async Task getTariffList_OverdueParameter_ReturnsList()
    {
        controller.getOverdueTariffList().Returns(EntityMaker.getATariffList());

        var actionResult = await actions.getTariffList("state:overdue");

        var result = assertAndGetOkResult(actionResult);
        var list = Assert.IsType<List<TariffEntity>>(result.Value);
        Assert.NotEmpty(list);
        Assert.Equal(2, list.Count);
    }

    [Theory]
    [InlineData("")]
    [InlineData("par1")]
    [InlineData("par1:par2")]
    [InlineData("par1:par2:par3:par4")]
    public async Task getTariffList_InvalidParameter_ReturnBadRequest(string parameter)
    {
        var actionResult = await actions.getTariffList(parameter);
        
        Assert.IsType<BadRequestObjectResult>(actionResult);
    }

    
    [Fact]
    public async Task getTariffTypeList_ReturnsList()
    {
        controller.getTariffTypeList().Returns(EntityMaker.getTariffTypeList());

        var actionResult = await actions.getTariffTypeList();

        var result = assertAndGetOkResult(actionResult);
        var list = Assert.IsType<List<TariffTypeEntity>>(result.Value);
        Assert.NotEmpty(list);
        Assert.Equal(2, list.Count);
    }
    
    [Fact]
    public async Task getTariffTypeList_ListNull_ReturnsException()
    {
        controller.getTariffTypeList().Returns([]);
        
        var actionResult = await actions.getTariffTypeList();

        var result = assertAndGetOkResult(actionResult);
        var list = Assert.IsType<List<TariffTypeEntity>>(result.Value);
        Assert.Empty(list);
    }

    
    [Fact]
    public async Task applyArrears_ArrearsApplied()
    {
        const int tariffId = 1;
        controller.applyArrears(tariffId).Returns(Task.FromResult(true));

        var actionResult = await actions.applyArrears(tariffId);

        assertAndGetOkResult(actionResult);
    }
    
    [Fact]
    public async Task applyArrears_InvalidId_ReturnsBadRequestObject()
    {
        var actionResult = await actions.applyArrears(-1);

        Assert.IsType<BadRequestObjectResult>(actionResult);
    }
    
    [Fact]
    public async Task applyArrears_ExceptionOccurs_ReturnsBadRequestObject()
    {
        controller.applyArrears(1).Returns(_ => throw new Exception("Internal error"));
        
        var actionResult = await actions.applyArrears(1);

        Assert.IsType<BadRequestObjectResult>(actionResult);
    }

    
    [Fact]
    public async Task saveTransaction_TransactionIsSave()
    {
        var dto = EntityMaker.getATransactionDto();
        controller.saveTransaction(DtoMapper.toEntity(dto)).Returns("tst-id");
        controller.exonerateArrears(DtoMapper.toEntity(dto.details!, "std-id")).Returns(Task.FromResult("string"));
        
        var actionResult = await actions.saveTransaction(dto);

        var result = assertAndGetOkResult(actionResult);
        Assert.NotNull(result.Value);
        var valueResult = result.Value.GetType().GetProperty("transactionId");
        Assert.NotNull(valueResult);
        var id = Assert.IsType<string>(valueResult.GetValue(result.Value));
        Assert.Equal("tst-id", id);
    }
    
    [Fact]
    public async Task saveTransaction_InvalidParameter_ReturnsException()
    {
        var dto = EntityMaker.getAIncorrectTransactionDto();
        controller.saveTransaction(DtoMapper.toEntity(dto)).Returns("id");
        controller.exonerateArrears(DtoMapper.toEntity(dto.details, "std-id"))
            .Returns(_ => throw new DbException("Any exception"));
        
        var actionResult = await actions.saveTransaction(dto);

        Assert.IsType<BadRequestObjectResult>(actionResult);
    }


    [Fact]
    public async Task getInvoice_ValidParameter_ReturnsInvoice()
    {
        const string transactionId = "tst-id";
        controller.getFullTransaction(transactionId).Returns(EntityMaker.getAInvoice());

        var actionResult = await actions.getInvoice(transactionId);

        var result = assertAndGetOkResult(actionResult);
        var dto = Assert.IsType<InvoiceDto>(result.Value);
        Assert.NotNull(dto);
    }
}