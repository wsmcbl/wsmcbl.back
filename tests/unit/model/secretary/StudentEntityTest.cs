using wsmcbl.src.model.secretary;

namespace wsmcbl.tests.unit.model.secretary;

public class StudentEntityTest
{
    [Fact]
    public void init_ShouldInitAttribute_WhenCalled()
    {
        var sut = new StudentEntity();
        
        sut.init();
        
        Assert.Empty(sut.schoolYear);
        Assert.True(sut.isActive);
    }
    
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
}