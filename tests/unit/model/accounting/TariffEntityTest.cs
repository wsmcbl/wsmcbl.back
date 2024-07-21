using wsmcbl.src.model.accounting;

namespace wsmcbl.tests.unit.model.accounting;

public class TariffEntityTest
{
    [Fact]
    public void checkDueDate_ShouldSetIsLateInTrue_When_WhenConditionIsValid()
    {
        var sut = new TariffEntity
        {
            dueDate = new DateOnly(2020, 1, 1)
        };

        sut.checkDueDate();
        
        Assert.True(sut.isLate);
    }
}