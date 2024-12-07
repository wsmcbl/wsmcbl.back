using NSubstitute;
using wsmcbl.src.model.dao;
using wsmcbl.src.model.secretary;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.model.secretary;

public class StudentEntityTest
{
    [Fact]
    public void fullName_ReturnsFullName()
    {
        var sut = new StudentEntity
        {
            name = "A",
            secondName = "B",
            surname = "C",
            secondSurname = "D"
        };

        var result = sut.fullName();

        Assert.IsType<string>(result);
        Assert.Equal("A B C D", result);
    }

    [Fact]
    public void update_ShouldUpdateEntity_WhenParameterIsValid()
    {
        var entity = TestEntityGenerator.aStudent("std1");
        
        var sut = new StudentEntity();
        
        sut.update(entity);
        
        Assert.Equal(entity.fullName(), sut.fullName());
        Assert.Equal(entity.isActive, sut.isActive);
        Assert.Equal(entity.sex, sut.sex);
        Assert.Equal(entity.birthday, sut.birthday);
        Assert.Equal(entity.diseases, sut.diseases);
        Assert.Equal(entity.religion, sut.religion);
        Assert.Equal(entity.address, sut.address);
    }

    [Fact]
    public async Task SaveChanges_ShouldAddElementIntoDaos_WhenParametersAreValid()
    {
        var daoFactory = Substitute.For<DaoFactory>();

        var sut = TestEntityGenerator.aStudent("std-01");
        
        await daoFactory.studentDao!.Received().updateAsync(sut);
        await daoFactory.studentTutorDao!.Received().updateAsync(sut.tutor);
        await daoFactory.studentFileDao!.Received().updateAsync(sut.file);
        await daoFactory.studentMeasurementsDao!.Received().updateAsync(sut.measurements);
        await daoFactory.studentParentDao!.Received().updateAsync(Arg.Any<StudentParentEntity>());
        await daoFactory.Received().execute();
    }
}