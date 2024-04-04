using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace wsmcbl.back.config;

public class RoutePrefixConvention(string prefix) : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        foreach (var selector in controller.Selectors)
        {
            if (selector.AttributeRouteModel != null)
            {
                selector.AttributeRouteModel.Template = $"{prefix}/{selector.AttributeRouteModel.Template}";
            }
        }
    }
}