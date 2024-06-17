using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace wsmcbl.src.utilities;

public class RoutePrefixConvention(string prefix) : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        foreach (var selector in controller.Selectors
                     .Select(p => p.AttributeRouteModel)
                     .Where(s => s != null))
        {
            selector!.Template = $"{prefix}/{selector.Template}";
        }
    }
}