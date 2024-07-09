using Microsoft.AspNetCore.Mvc;
using wsmcbl.src.controller.api;
using wsmcbl.src.controller.business;
using wsmcbl.src.model.secretary;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.controller.api;

public class CreateOfficialEnrollmentActionsTest
{
    private readonly ICreateOfficialEnrollmentController controller;
    private readonly CreateOfficialEnrollmentActions actions;

    public CreateOfficialEnrollmentActionsTest()
    {
        controller = Substitute.For<ICreateOfficialEnrollmentController>();
        actions = new CreateOfficialEnrollmentActions(controller);
    }
    
    private static OkObjectResult assertAndGetOkResult(IActionResult actionResult) =>
        Assert.IsType<OkObjectResult>(actionResult);
    
    
    [Fact]
    public async Task getStudentList_ReturnsList()
    {
        var entityGenerator = new TestEntityGenerator();
        controller.getStudentList().Returns(entityGenerator.aSecretaryStudentList());

        var actionResult = await actions.getStudentList();

        var result = assertAndGetOkResult(actionResult);
        var list = Assert.IsType<List<StudentEntity>>(result.Value);
        Assert.NotEmpty(list);
        Assert.Equal(2, list.Count);
    }
    
    
    [Fact]
    public async Task getStudentList_EmptyList()
    {
        var studentList = new List<StudentEntity>();
        controller.getStudentList().Returns(studentList);
        
        var actionResult = await actions.getStudentList();

        var result = assertAndGetOkResult(actionResult);
        var list = Assert.IsType<List<StudentEntity>>(result.Value);
        Assert.Empty(list);
    }
}