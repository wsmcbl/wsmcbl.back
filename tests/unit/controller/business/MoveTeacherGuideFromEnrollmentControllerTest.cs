using NSubstitute;
using wsmcbl.src.controller.business;
using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.controller.business;

public class MoveTeacherGuideFromEnrollmentControllerTest
{
    private readonly MoveTeacherGuideFromEnrollmentController sut;
    private readonly DaoFactory daoFactory;

    public MoveTeacherGuideFromEnrollmentControllerTest()
    {
        daoFactory = Substitute.For<DaoFactory>();
        sut = new MoveTeacherGuideFromEnrollmentController(daoFactory);
    }
    
    [Fact]
    public async Task getTeacherList_ShouldReturnsTeacherList_WhenCalled()
    {
        var teacherDao = Substitute.For<ITeacherDao>();
        teacherDao.getAll().Returns(TestEntityGenerator.aTeacherList());
        daoFactory.teacherDao.Returns(teacherDao);

        var result = await sut.getTeacherList();

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task getEnrollmentById_ShouldReturnsEnrollment_WhenCalled()
    {
        var enrollment = TestEntityGenerator.aEnrollment();
        var enrollmentDao = Substitute.For<IEnrollmentDao>();
        enrollmentDao.getById(enrollment.enrollmentId!).Returns(enrollment);
        daoFactory.enrollmentDao.Returns(enrollmentDao);
        
        var result = await sut.getEnrollmentById(enrollment.enrollmentId!);

        Assert.NotNull(result);
        Assert.Equal(enrollment.enrollmentId, result.enrollmentId);
    }

    [Fact]
    public async Task getEnrollmentById_ShouldThrowException_WhenEnrollmentNotExist()
    {
        await Assert.ThrowsAsync<EntityNotFoundException>(() => sut.getEnrollmentById("empty"));
    }

    [Fact]
    public async Task getTeacherById_ShouldReturnsTeacher_WhenCalled()
    {
        var teacher = TestEntityGenerator.aTeacher();
        var teacherDao = Substitute.For<ITeacherDao>();
        teacherDao.getById(teacher.teacherId!).Returns(teacher);
        daoFactory.teacherDao.Returns(teacherDao);
        
        var result = await sut.getTeacherById(teacher.teacherId!);

        Assert.NotNull(result);
        Assert.Equal(teacher.teacherId, result.teacherId);
    }

    [Fact]
    public async Task getTeacherId_ShouldThrowException_WhenTeacherNotExist()
    {
        await Assert.ThrowsAsync<EntityNotFoundException>(() => sut.getTeacherById("empty"));
    }

    [Fact]
    public async Task assignTeacherGuide_ShouldAssignTeacher_WhenCalledOldTeacherNotExist()
    {
        var newTeacher = TestEntityGenerator.aTeacher();
        newTeacher.isGuide = false;
        var enrollment = TestEntityGenerator.aEnrollment();
        
        var teacherDao = Substitute.For<ITeacherDao>();
        teacherDao.getById(newTeacher.teacherId!).Returns(newTeacher);
        daoFactory.teacherDao.Returns(teacherDao);
        
        await sut.assignTeacherGuide(newTeacher, enrollment);

        await daoFactory.Received().ExecuteAsync();
    }

    [Fact]
    public async Task assignTeacherGuide_ShouldAssignTeacher_WhenCalledOldTeacherExist()
    {
        var newTeacher = TestEntityGenerator.aTeacher();
        newTeacher.isGuide = false;
        var enrollment = TestEntityGenerator.aEnrollment();
        
        var oldTeacher = TestEntityGenerator.aTeacher();
        oldTeacher.teacherId = "oldTeacher";
        
        var teacherDao = Substitute.For<ITeacherDao>();
        teacherDao.getById(newTeacher.teacherId!).Returns(newTeacher);
        teacherDao.getByEnrollmentId(enrollment.enrollmentId!).Returns(oldTeacher);
        
        daoFactory.teacherDao.Returns(teacherDao);
        
        await sut.assignTeacherGuide(newTeacher, enrollment);

        await daoFactory.Received(2).ExecuteAsync();
    }
}