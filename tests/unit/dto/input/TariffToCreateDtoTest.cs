using wsmcbl.src.dto.secretary;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.dto.input;

public class TariffToCreateDtoTest
{
    [Fact]
    public void init_ShouldReturnEntity_WhenCalled()
    {
        var entity = TestEntityGenerator.aTariff();

        var result = new TariffToCreateDto(entity);
        
        Assert.NotNull(result);
        Assert.NotNull(result.dueDate);
        Assert.NotEmpty(result.concept);
        Assert.NotEmpty(result.schoolYear);
        Assert.True(result.type > 0);
        Assert.True(result.amount > 0);
        Assert.True(result.modality > 0);
    }
    
    
    [Fact]
    public void toEntity_ShouldReturnTariffEntity_WhenCalled()
    {
        var sut = TestDtoInputGenerator.aTariffDto();

        var result = sut.toEntity();

        Assert.NotNull(result);
        Assert.NotNull(result.dueDate);
        Assert.NotEmpty(result.concept);
        Assert.NotEmpty(result.schoolYear!);
        Assert.True(result.type > 0);
        Assert.True(result.amount > 0);
        Assert.True(result.educationalLevel > 0);
    }
}