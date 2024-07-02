using wsmcbl.src.model.secretary;

namespace wsmcbl.tests.unit.model.secretary;

public class StudentEntityTest
{
    private StudentEntity? entity;

    [Fact]
    public void fullName_ReturnsFullName()
    {
        entity = new StudentEntity
        {
            name = "A",
            secondName = "B",
            surname = "C",
            secondSurname = "D"
        };

        var result = entity.fullName();

        Assert.IsType<string>(result);
        Assert.Equal("A B C D", result);
    }
}