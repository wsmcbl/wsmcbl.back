using wsmcbl.src.model.accounting;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.model.accounting;

public class StudentEntityTest
{

    [Fact]
    public async Task loadDebtHistory_ShouldNotLoadDebtHistory_WhenIDebtHistoryDaoIsNull()
    {
        var sut = new TestEntityGenerator().aStudent("std-1");

        await sut.loadDebtHistory(null);
        
        Assert.Null(sut.debtHistory);
    }
    
    [Fact]
    public void calculateDiscount_ShouldReturnParameter_WhenDiscountIsNull()
    {
        var sut = new TestEntityGenerator().aStudent("std-1");
        sut.discount = null;

        var result = sut.calculateDiscount(1000);
        
        Assert.Equal(1000, result);
    }
    
    [Fact]
    public void getDiscount_ShouldReturnZero_WhenDiscountIsNull()
    {
        var sut = new TestEntityGenerator().aStudent("std-1");
        sut.discount = null;

        var result = sut.getDiscount();
        
        Assert.Equal(0, result);
    }
}