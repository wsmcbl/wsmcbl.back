using wsmcbl.src.dto.secretary;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.dto.input;

public class GradeToCreateDtoTest
{
    [Fact]
    public void init_ShouldReturnDto_WhenCalled()
    {
        var entity = TestEntityGenerator.aGrade("gd1");

        var result = new GradeToCreateDto(entity);
        
        Assert.NotNull(result);
        Assert.NotNull(result.schoolYear);
        Assert.NotNull(result.modality);
        Assert.NotNull(result.label);
    }
    
    
    [Fact]
    public void toEntity_ShouldReturnGradeEntity_WhenCalled()
    {
        var sut = TestDtoInputGenerator.aGradeDto();

        var result = sut.toEntity();
        
        Assert.NotNull(result);
        Assert.NotNull(result.schoolYear);
        Assert.NotNull(result.modality);
        Assert.NotNull(result.label);
    }
}