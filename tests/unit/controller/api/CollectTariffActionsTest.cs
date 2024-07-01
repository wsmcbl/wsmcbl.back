using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using wsmcbl.src.controller.api;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.output;
using wsmcbl.src.exception;
using wsmcbl.src.model.accounting;

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
    
    [Fact]
    public async Task getStudentList_ReturnsList()
    {
        controller.getStudentsList().Returns(EntityMaker.getObjectStudentList());
        
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
        controller.getStudent(studentId).Returns(EntityMaker.getObjetStudent(studentId));

        var actionResult = await actions.getStudentById(studentId);

        var result = assertAndGetOkResult(actionResult);
        var studentDto = Assert.IsType<StudentDto>(result.Value);
        Assert.NotNull(studentDto);
    }

    [Fact]
    public void getStudentById_InvalidId_ReturnException()
    {
        Assert.ThrowsAsync<EntityNotFoundException>(() => actions.getStudentById("id2"));
    }

    [Fact]
    public void getStudentById_NullParameter_ReturnException()
    {
        Assert.ThrowsAsync<Exception>(() => actions.getStudentById(null));
    }

    [Fact]
    public async Task getTariff_ValidStudentParameter_ReturnsTariffList()
    {
        var studentId = "id1";
        controller.getTariffListByStudent(studentId).Returns(EntityMaker.getObjectTariffList());

        var actionResult = await actions.getTariffs($"student:{studentId}");

        var result = assertAndGetOkResult(actionResult);
        var list = Assert.IsType<List<TariffEntity>>(result.Value);
        Assert.NotEmpty(list);
        Assert.Equal(2, list.Count);
    }

    [Fact]
    public async Task getTariff_OverdueParameter_ReturnsTariffList()
    {
        controller.getOverdueTariffList().Returns(EntityMaker.getObjectTariffList());

        var actionResult = await actions.getTariffs("state:overdue");

        var result = assertAndGetOkResult(actionResult);
        var list = Assert.IsType<List<TariffEntity>>(result.Value);
        Assert.NotEmpty(list);
        Assert.Equal(2, list.Count);
    }
    
    private static OkObjectResult assertAndGetOkResult(IActionResult actionResult) =>
        Assert.IsType<OkObjectResult>(actionResult);
}