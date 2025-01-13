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
    public async Task getNewSchoolYearInformation_ShouldNewSchoolInformation_WhenCalled()
    {
        var schoolyear = TestEntityGenerator.aSchoolYear();
        var dao = Substitute.For<ISchoolyearDao>();
        dao.getOrCreateNewSchoolyear().Returns(schoolyear);

        List<TariffDataEntity> tariffData = [TestEntityGenerator.aTariffData()];
        List<DegreeDataEntity> gradeData = [TestEntityGenerator.aDegreeData()];

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
         var gradeList = TestEntityGenerator.aDegreeList();
         var tariffList = TestEntityGenerator.aTariffList();

         var currentSchoolyear = TestEntityGenerator.aSchoolYear();
         var schoolyearDao = Substitute.For<ISchoolyearDao>();
         schoolyearDao.getCurrentSchoolyear().Returns(currentSchoolyear);
         daoFactory.schoolyearDao.Returns(schoolyearDao);

         sut = new UpdateOfficialEnrollmentController(daoFactory);

         await sut.createSchoolYear(gradeList, tariffList);

         await daoFactory.Received().execute();
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