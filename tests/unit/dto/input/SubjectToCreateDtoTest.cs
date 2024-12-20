using wsmcbl.src.dto.secretary;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.dto.input;

public class SubjectToCreateDtoTest
{
    [Fact]
    public void init_ShouldReturnDto_WhenCalled()
    {
        var entity = TestEntityGenerator.aSubject();

        var result = new SubjectToCreateDto(entity);
        
        Assert.NotNull(result);
        Assert.NotNull(result.name);
        Assert.NotEmpty(result.name);
        Assert.True(result.semester > 0);
    }
    
    
    [Fact]
    public void toEntity_ShouldReturnSubjectEntity_WhenCalled()
    {
        var sut = TestDtoInputGenerator.aSubjectDto();

        var result = sut.toEntity();
        
        Assert.NotNull(result);
        Assert.NotNull(result.name);
        Assert.NotEmpty(result.name);
        Assert.True(result.semester > 0);
    }
}