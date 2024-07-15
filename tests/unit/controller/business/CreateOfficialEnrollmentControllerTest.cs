using wsmcbl.src.controller.business;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.controller.business;

public class CreateOfficialEnrollmentControllerTest
{
    private readonly CreateOfficialEnrollmentController controller;
    private readonly DaoFactory daoFactory;
    private readonly IStudentDao studentDao;

    public CreateOfficialEnrollmentControllerTest()
    {
        studentDao = Substitute.For<IStudentDao>();
        daoFactory = Substitute.For<DaoFactory>();
        controller = new CreateOfficialEnrollmentController(daoFactory);
    }


    [Fact]
    public async Task getStudentList()
    {
        var entityGenerator = new TestEntityGenerator();
        studentDao.getAll().Returns(entityGenerator.aSecretaryStudentList());
        daoFactory.secretaryStudentDao.Returns(studentDao);

        var result = await controller.getStudentList();

        var list = Assert.IsType<List<StudentEntity>>(result);
        Assert.NotEmpty(list);
        Assert.Equal(2, list.Count);
    }
}