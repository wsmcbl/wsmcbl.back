using wsmcbl.src.controller.business;
using wsmcbl.src.model.accounting;
using wsmcbl.src.model.dao;

namespace wsmcbl.tests.unit.controller.business;

public class CollectTariffControllerTest
{
    private readonly CollectTariffController controller;
    private readonly DaoFactory daoFactory;
    private readonly IStudentDao studentDao;

    public CollectTariffControllerTest()
    {
        studentDao = Substitute.For<IStudentDao>();
        daoFactory = Substitute.For<DaoFactory>();
        controller = new CollectTariffController(daoFactory);
    }

    [Fact]
    public async Task getStudentsList_ReturnsList()
    {
        studentDao.getAll().Returns(EntityMaker.getAStudentList());
        daoFactory.studentDao<StudentEntity>().Returns(studentDao);

        var result = await controller.getStudentsList();

        var list = Assert.IsType<List<StudentEntity>>(result);
        Assert.NotEmpty(list);
        Assert.Equal(2, list.Count);
    }
}