using NSubstitute;
using wsmcbl.src.controller.business;
using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.controller.business;

public class UpdateOfficialEnrollmentControllerTest
{
    private UpdateOfficialEnrollmentController sut;
    private readonly DaoFactory daoFactory;

    public UpdateOfficialEnrollmentControllerTest()
    {
        daoFactory = Substitute.For<DaoFactory>();
        sut = new UpdateOfficialEnrollmentController(daoFactory);
    }
    
    [Fact]
    public async Task getGradeById_GradeNotFound_ReturnException()
    {
        await Assert.ThrowsAsync<EntityNotFoundException>(() => sut.getDegreeById("gd-1"));
    }
    
    [Fact]
    public async Task getGradeById_ReturnGrade()
    {
        var grade = TestEntityGenerator.aDegree("gd-1");
        var gradeDao = Substitute.For<IDegreeDao>();
        gradeDao.getById("gd-1").Returns(grade);
        daoFactory.degreeDao.Returns(gradeDao);

        var result = await sut.getDegreeById("gd-1");

        Assert.NotNull(result);
        Assert.Equivalent(grade, result);
    } 
}