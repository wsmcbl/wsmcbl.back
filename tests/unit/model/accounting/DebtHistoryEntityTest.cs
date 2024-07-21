using wsmcbl.src.model.accounting;

namespace wsmcbl.tests.unit.model.accounting;

public class DebtHistoryEntityTest
{
    [Fact]
    public void getDebtBalance_ShouldReturnZero_WhenDebtIsLessThanZero()
    {
        var sut = new DebtHistoryEntity
        {
            amount = 50,
            debtBalance = 200
        };

        var result = sut.getDebtBalance();
        
        Assert.Equal(0, result);
    }
    
    [Fact]
    public void havePayments_ReturnsTrue()
    {
        var sut = new DebtHistoryEntity
        {
            debtBalance = 45
        };

        var result = sut.havePayments();
        
        Assert.True(result);
    }
}