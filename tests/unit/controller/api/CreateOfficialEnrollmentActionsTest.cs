using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using wsmcbl.src.controller.api;
using wsmcbl.src.controller.business;
using wsmcbl.src.dto.output;
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
    public async Task getGradeList_ReturnsList()
    {
        var gradeList = TestDtoOutputGenerator.aGradeList();
        controller.getGradeList().Returns(gradeList);

        var actionResult = await actions.getGradeList();

        var result = assertAndGetOkResult(actionResult);
        var list = Assert.IsType<List<GradeBasicDto>>(result.Value);
        Assert.Equivalent(gradeList.mapListToBasicDto(), list);
    }
    

    [Fact]
    public async Task getTeacherList_ReturnsList()
    {
        var teacherList = TestDtoOutputGenerator.aTeacherList();
        controller.getTeacherList().Returns(teacherList);

        var actionResult = await actions.getTeacherList();

        var result = assertAndGetOkResult(actionResult);
        var list = Assert.IsType<List<TeacherBasicDto>>(result.Value);
        Assert.Equivalent(teacherList.mapListToBasicDto(), list);
    }
    
    [Fact]
    public async Task createTariff_TariffCreated()
    {
        var tariffDataDto = TestDtoInputGenerator.aTariffDataDto();

        var actionResult = await actions.createTariff(tariffDataDto);

        Assert.IsType<OkResult>(actionResult);
    }
    

    [Fact]
    public async Task createSubject_SubjectCreated()
    {
        var subjectdDataDto = TestDtoInputGenerator.aSubjectDataDto();

        var actionResult = await actions.createSubject(subjectdDataDto);

        Assert.IsType<OkResult>(actionResult);
    }

    [Fact]
    public async Task createSchoolYear_SchoolyearCreated()
    {
        var schoolyearDto = TestDtoInputGenerator.aSchoolYearToCreateDto();

        var actionResult = await actions.createSchoolYear(schoolyearDto);

        Assert.IsType<OkResult>(actionResult);
    }
    
    
    [Theory]
    [InlineData("")]
    [InlineData("other")]
    public async Task getSchoolYears_InvalidParameter_ReturnsBadRequestResult(string query)
    {
        var actionResult = await actions.getSchoolYears(query);
        
        Assert.IsType<BadRequestObjectResult>(actionResult);
    }

    [Fact]
    public async Task getSchoolYears_ParameterNew_ReturnsNewSchoolYear()
    {
        var schoolyear = TestDtoOutputGenerator.aSchoolYearList()[0];
        controller.getNewSchoolYearInformation().Returns(schoolyear);

        var actionResult = await actions.getSchoolYears("new");

        var result = assertAndGetOkResult(actionResult);
        var value = Assert.IsType<SchoolYearDto>(result.Value);
        Assert.Equivalent(schoolyear.mapToDto(), value);
    }

    [Fact]
    public async Task getSchoolYears_ParameterAll_ReturnsList()
    {
        var schoolyearList = TestDtoOutputGenerator.aSchoolYearList();
        controller.getSchoolYearList().Returns(schoolyearList);

        var actionResult = await actions.getSchoolYears("all");

        var result = assertAndGetOkResult(actionResult);
        var values = Assert.IsType<List<SchoolYearBasicDto>>(result.Value);
        Assert.Equivalent(schoolyearList.mapListToDto(), values);
    }

    [Fact]
    public async Task updateEnrollment_EnrollmentUpdate()
    {
        var enrollmentDto = TestDtoInputGenerator.aEnrollmentDto();
        
        var actionResult = await actions.updateEnrollment(enrollmentDto);

        Assert.IsType<OkResult>(actionResult);
    }

    [Fact]
    public async Task createEnrollment_EnrollmentCreated()
    {
        var enrollmentDto = TestDtoInputGenerator.aEnrollmentToCreateDto();
        
        var actionResult = await actions.createEnrollment(enrollmentDto);

        Assert.IsType<OkResult>(actionResult);
    }

    [Fact]
    public async Task getGradeById_ReturnsGrade()
    {
        var grade = TestEntityGenerator.aGrade("gd-1");
        controller.getGradeById("gd-1").Returns(grade);

        var actionResult = await actions.getGradeById("gd-1");

        var result = assertAndGetOkResult(actionResult);
        var value = Assert.IsType<GradeDto>(result.Value);
        Assert.Equivalent(grade.mapToDto(), value);
    }

    [Fact]
    public async Task saveStudent_StudentSaved()
    {
        var studentDto = TestDtoInputGenerator.aStudentDto();

        var actionResult = await actions.saveStudent(studentDto);

        Assert.IsType<OkResult>(actionResult);
    }
    
    
    [Fact]
    public async Task getStudentList_ReturnsList()
    {
        controller.getStudentList().Returns(TestEntityGenerator.aSecretaryStudentList());

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