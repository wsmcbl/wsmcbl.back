using NSubstitute;
using wsmcbl.src.controller.business;
using wsmcbl.src.exception;
using wsmcbl.src.model.academy;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.controller.business;

public class CreateEnrollmentControllerTest
{
    private CreateEnrollmentController sut;
    private readonly DaoFactory daoFactory;

    public CreateEnrollmentControllerTest()
    {
        daoFactory = Substitute.For<DaoFactory>();
        sut = new CreateEnrollmentController(daoFactory);
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
        var grade = TestEntityGenerator.aDegree("gd-1");
        
        var gradeDao = Substitute.For<IDegreeDao>();
        gradeDao.getById("gd-1").Returns(grade);

        var enrollmentDao = Substitute.For<IEnrollmentDao>();
        
        daoFactory.degreeDao.Returns(gradeDao);
        daoFactory.enrollmentDao.Returns(enrollmentDao);
        
        await sut.createEnrollments("gd-1", 1);
        
        daoFactory.enrollmentDao!.Received().create(grade.enrollmentList!.First());
        daoFactory.Received().Detached(grade.enrollmentList!.First().subjectList!.First());
    }   
}