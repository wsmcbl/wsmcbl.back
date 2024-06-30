using NSubstitute;
using wsmcbl.src.controller.api;
using wsmcbl.src.controller.business;

namespace wsmcbl.test.unit.controller.api;

public class CollectTariffActionsTest
{
    private readonly CollectTariffActions actions;

    public CollectTariffActionsTest()
    {
        var controller = Substitute.For<ICollectTariffController>();
        actions = new CollectTariffActions(controller);
    }
    
    [Fact]
    public async Task getStudentList_ValidPepiline_ReturnOk()
    {
        var list = await actions.getStudentList();
        
        Assert.Null(list);
    }
}