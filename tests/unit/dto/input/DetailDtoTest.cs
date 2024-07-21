using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.dto.input;

public class DetailDtoTest
{
    [Fact]
    public void toEntity_ShouldReturnTransactionTariffEntity_WhenCalled()
    {
        var sut = TestDtoInputGenerator.aDetailDto();

        var result = sut.toEntity();
        
        Assert.NotNull(result);
        Assert.True(result.tariffId != 0);
        Assert.True(result.amount > 0);
    }
}