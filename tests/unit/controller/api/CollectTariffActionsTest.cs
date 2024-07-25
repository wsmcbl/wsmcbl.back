using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using wsmcbl.src.controller.api;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.input;
using wsmcbl.src.dto.output;
using wsmcbl.src.exception;
using wsmcbl.src.model.accounting;
using wsmcbl.tests.utilities;
using DtoMapper = wsmcbl.src.dto.input.DtoMapper;
using OutDtoMapper = wsmcbl.src.dto.output.DtoMapper;
using StudentDto = wsmcbl.src.dto.output.StudentDto;

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
        var initList = TestEntityGenerator.aStudentList();
        controller.getStudentsList().Returns(initList);
        
        var actionResult = await actions.getStudentList();

        var result = assertAndGetOkResult(actionResult);
        var list = Assert.IsType<List<StudentBasicDto>>(result.Value);
        Assert.NotEmpty(list);
        Assert.Equivalent(initList.mapListTo(), list);
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
        var student = TestEntityGenerator.aStudent("std-1");
        controller.getStudent(student.studentId!).Returns(student);

        var actionResult = await actions.getStudentById(student.studentId!);

        var result = assertAndGetOkResult(actionResult);
        var studentDto = Assert.IsType<StudentDto>(result.Value);
        Assert.NotNull(studentDto);
        Assert.Equivalent(OutDtoMapper.mapToDto(student), studentDto);
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
        var initList = TestEntityGenerator.aTariffList();
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
        var listInit = TestEntityGenerator.aTariffList();
        controller.getOverdueTariffList().Returns(listInit);

        var actionResult = await actions.getTariffList("state:overdue");

        var result = assertAndGetOkResult(actionResult);
        var list = Assert.IsType<List<TariffEntity>>(result.Value);
        Assert.NotEmpty(list);
        Assert.Equal(listInit, list);
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
        controller.getTariffTypeList().Returns(TestEntityGenerator.aTariffTypeList());

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
        var dto = Substitute.For<TransactionDto>();
        
        var entity = new TransactionEntity();
        dto.toEntity().Returns(entity);
        
        var detail = new List<DebtHistoryEntity>();
        dto.getDetailToApplyArrear().Returns(detail);

        controller.saveTransaction(entity, detail).Returns("tst-id");
        
        var actionResult = await actions.saveTransaction(dto);

        var result = assertAndGetOkResult(actionResult);
        Assert.NotNull(result.Value);
        var valueResult = result.Value.GetType().GetProperty("transactionId");
        Assert.NotNull(valueResult);
        var id = Assert.IsType<string>(valueResult.GetValue(result.Value));
        Assert.Equal("tst-id", id);
    }
    

    [Fact]
    public async Task getInvoice_ValidParameter_ReturnsInvoice()
    {
        const string transactionId = "tst-id";
        controller.getFullTransaction(transactionId).Returns(TestEntityGenerator.aTupleInvoice());

        var actionResult = await actions.getInvoice(transactionId);

        var result = assertAndGetOkResult(actionResult);
        var dto = Assert.IsType<InvoiceDto>(result.Value);
        Assert.NotNull(dto);
    }
}