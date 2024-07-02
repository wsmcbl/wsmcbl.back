using wsmcbl.src.model.accounting;

namespace wsmcbl.tests.unit.model.accounting;

public class DebtHistoryEntityTest
{
    private DebtHistoryEntity? entity;

    [Fact]
    public void havePayments_ReturnsTrue()
    {
        entity = new DebtHistoryEntity
        {
            debtBalance = 45
        };

        var result = entity.havePayments();
        
        Assert.True(result);
    }
}