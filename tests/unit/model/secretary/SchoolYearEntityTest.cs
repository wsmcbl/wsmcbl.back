using wsmcbl.src.model.secretary;
using wsmcbl.tests.utilities;

namespace wsmcbl.tests.unit.model.secretary;

public class SchoolYearEntityTest
{
    [Fact]
    public void setTariffDataList_ShouldSetTariffDataList_WhenDataIsProvide()
    {
        var tariffDataList = new List<TariffDataEntity>{ TestEntityGenerator.aTariffData() };
        tariffDataList[0].dueDate = new DateOnly(2020, 1, 1);
        
        var sut = new SchoolYearEntity
        {
            label = DateTime.Now.Year.ToString()
        };

        var newDate = new DateOnly(int.Parse(sut.label), 1, 1);
        
        sut.setTariffDataList(tariffDataList);
        
        Assert.Equivalent(newDate, sut.tariffList![0].dueDate);
    }
}