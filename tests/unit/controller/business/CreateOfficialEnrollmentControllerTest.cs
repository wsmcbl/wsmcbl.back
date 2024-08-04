using NSubstitute;
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


    [Fact]
    public async Task getNewSchoolYearInformation_ShouldNewSchoolInformation_WhenCalled()
    {
        var schoolyear = TestEntityGenerator.aSchoolYear();
        var dao = Substitute.For<ISchoolyearDao>();
        dao.getNewSchoolYear().Returns(schoolyear);

        List<TariffDataEntity> tariffData = [TestEntityGenerator.aTariffData()];
        List<DegreeDataEntity> gradeData = [TestEntityGenerator.aGradeData()];

        var tariffDataDao = Substitute.For<ITariffDataDao>();
        tariffDataDao.getAll().Returns(tariffData);

        var gradaDataDao = Substitute.For<IDegreeDataDao>();
        gradaDataDao.getAll().Returns(gradeData);

        daoFactory.schoolyearDao.Returns(dao);
        daoFactory.tariffDataDao.Returns(tariffDataDao);
        daoFactory.degreeDataDao.Returns(gradaDataDao);
        
        var result = await sut.getNewSchoolYearInformation();

        Assert.NotNull(result);
    }

    [Fact]
    public async Task createSubject_ShouldCreateTariff_WhenParameterIsValid()
    {
        var subject = TestEntityGenerator.aSubjectData();
        var dao = Substitute.For<ISubjectDataDao>();
        daoFactory.subjectDataDao.Returns(dao);

        await sut.createSubject(subject);
        
        dao.Received(1).create(subject);
        await daoFactory.Received(1).execute();
    }
    
    [Fact]
    public async Task createTariff_ShouldCreateTariff_WhenParameterIsValid()
    {
        var tariff = TestEntityGenerator.aTariffData();
        var dao = Substitute.For<ITariffDataDao>();
        daoFactory.tariffDataDao.Returns(dao);

        await sut.createTariff(tariff);
        
        dao.Received(1).create(tariff);
        await daoFactory.Received(1).execute();
    }

    [Fact]
    public async Task createSchoolYear_ShouldReturnException_WhenParameterAreNotValid()
    {
        await Assert.ThrowsAsync<BadRequestException>(() => sut.createSchoolYear([], []));
    }
    
    [Fact]
    public async Task createSchoolYear_ShouldCreateSchoolYear_WhenParametersAreValid()
    {
         var gradeList = TestEntityGenerator.aGradeList();
         var tariffList = TestEntityGenerator.aTariffList();

         await sut.createSchoolYear(gradeList, tariffList);

         await daoFactory.Received(1).execute();
    }
    
    [Fact]
    public async Task getSchoolYearList_ShouldReturnsSchoolYearList_WhenCalled()
    {
        var list = TestEntityGenerator.aSchoolYearList();
        var dao = Substitute.For<ISchoolyearDao>();
        dao.getAll().Returns(list);
        daoFactory.schoolyearDao.Returns(dao);

        var result = await sut.getSchoolYearList();

        Assert.NotNull(result);
        Assert.Equivalent(list, result);
    }

    
    [Fact]
    public async Task getGradeList_ShouldReturnsGradeList_WhenCalled()
    {
        var list = TestEntityGenerator.aGradeList();
        var dao = Substitute.For<IDegreeDao>();
        dao.getAll().Returns(list);
        daoFactory.degreeDao.Returns(dao);

        var result = await sut.getGradeList();

        Assert.NotNull(result);
        Assert.Equivalent(list, result);
    }

    [Fact]
    public async Task getTeacherList_ShouldReturnsTeacherList_WhenCalled()
    {
        var teacherList = TestEntityGenerator.aTeacherList();
        var teacherDao = Substitute.For<ITeacherDao>();
        teacherDao.getAll().Returns(teacherList);
        daoFactory.teacherDao.Returns(teacherDao);

        var result = await sut.getTeacherList();

        Assert.NotNull(result);
        Assert.Equivalent(teacherList, result);
    }

    
    
    [Fact]
    public async Task createEnrollments_ShouldThrowException_WhenGradeNotExist()
    {
        await Assert.ThrowsAsync<EntityNotFoundException>(() => sut.createEnrollments("gd-1", 1));
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(8)]
    public async Task createEnrollments_ShouldThrowException_WhenQuantityIsNotValid(int quantity)
    {
        await Assert.ThrowsAsync<BadRequestException>(() => sut.createEnrollments("gd-1", quantity));
    }

    [Fact]
    public async Task createEnrollments_ShouldCreateEnrollments_WhenParametersIsProvide()
    {
        var grade = TestEntityGenerator.aGrade("gd-1");
        
        var gradeDao = Substitute.For<IDegreeDao>();
        gradeDao.getById("gd-1").Returns(grade);

        var enrollmentDao = Substitute.For<IEnrollmentDao>();
        
        daoFactory.degreeDao.Returns(gradeDao);
        daoFactory.enrollmentDao.Returns(enrollmentDao);
        
        await sut.createEnrollments("gd-1", 1);
        
        daoFactory.enrollmentDao!.Received().create(grade.enrollments.First());
        daoFactory.Received().Detached(grade.enrollments.First().subjectList!.First());
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
        var gradeDao = Substitute.For<IDegreeDao>();
        gradeDao.getById("gd-1").Returns(grade);
        daoFactory.degreeDao.Returns(gradeDao);

        var result = await sut.getGradeById("gd-1");

        Assert.NotNull(result);
        Assert.Equivalent(grade, result);
    }
}