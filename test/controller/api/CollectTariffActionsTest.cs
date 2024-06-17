using wsmcbl.src.controller.api;
using wsmcbl.src.controller.business;

namespace wsmcbl.test.controller.api;


public class CollectTariffActionsTest
{

    private ICollectTariffController controller;
    
    [Fact]
    public async Task getStudentListTest()
    {
        controller = new CollectTariffControllerTest();
        var actions = new CollectTariffActions(controller);

        await actions.applyArrears(1);
    }
}