using wsmcbl.src.controller.business;
using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;
using wsmcbl.tests.utilities;
using StudentEntity = wsmcbl.src.model.secretary.StudentEntity;

namespace wsmcbl.tests.unit.controller.business;

public class CreateOfficialEnrollmentControllerTest
{
    private readonly CreateOfficialEnrollmentController sut;
    private readonly DaoFactory daoFactory;

    public CreateOfficialEnrollmentControllerTest()
    {
        daoFactory = Substitute.For<DaoFactory>();
        sut = new CreateOfficialEnrollmentController(daoFactory);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(8)]
    public async Task createEnrollments_ShouldThrowException_WhenQuantityIsNotValid(int quantity)
    {
        await Assert.ThrowsAsync<ArgumentException>(() => sut.createEnrollments("gd-1", quantity));
    }

    [Fact]
    public async Task createEnrollments_ShouldCreateEnrollments_WhenParametersIsProvide()
    {
        var grade = TestEntityGenerator.aGrade("gd-1");
        
        var gradeDao = Substitute.For<IGradeDao>();
        gradeDao.getById("gd-1").Returns(grade);

        var enrollmentDao = Substitute.For<IEnrollmentDao>();
        
        daoFactory.gradeDao.Returns(gradeDao);
        daoFactory.enrollmentDao.Returns(enrollmentDao);
        
        await sut.createEnrollments("gd-1", 1);
        
        daoFactory.enrollmentDao!.Received().create(grade.enrollments.First());
        daoFactory.Received().Detached(grade.enrollments.First().subjectList.First());
    }

    [Fact]
    public async Task getGradeById_GradeNotFound_ReturnException()
    {
        await Assert.ThrowsAsync<EntityNotFoundException>(() => sut.getGradeById("gd-1"));
    }
    
    [Fact]
    public async Task getGradeById_ReturnGrade()
    {
        var grade = TestEntityGenerator.aGrade("gd-1");
        var gradeDao = Substitute.For<IGradeDao>();
        gradeDao.getById("gd-1").Returns(grade);
        daoFactory.gradeDao.Returns(gradeDao);

        var result = await sut.getGradeById("gd-1");

        Assert.NotNull(result);
        Assert.Equivalent(grade, result);
    }
    

    [Fact]
    public async Task getStudentList()
    {
        var entityGenerator = new TestEntityGenerator();

        var studentDao = Substitute.For<IStudentDao>();
        studentDao.getAll().Returns(entityGenerator.aSecretaryStudentList());
        daoFactory.secretaryStudentDao.Returns(studentDao);

        var result = await sut.getStudentList();

        var list = Assert.IsType<List<StudentEntity>>(result);
        Assert.NotEmpty(list);
        Assert.Equal(2, list.Count);
    }
}