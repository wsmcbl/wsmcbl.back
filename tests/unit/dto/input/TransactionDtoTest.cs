using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.dto.input;

public class TransactionDtoTest
{
    [Fact]
    public void toEntity_ShouldReturnTransactionEntity_WhenCalled()
    {
        var sut = TestDtoInputGenerator.aTransactionDto();

        var result = sut.toEntity();
        
        Assert.NotNull(result);
        Assert.NotNull(result.studentId);
        Assert.NotNull(result.cashierId);
        Assert.Equal(0, result.total);
        Assert.Equal(sut.dateTime, result.date);
    }
}