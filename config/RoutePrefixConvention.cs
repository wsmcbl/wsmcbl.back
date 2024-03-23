using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace wsmcbl.back.config;

public class RoutePrefixConvention : IControllerModelConvention
{
    private readonly string prefix;

    public RoutePrefixConvention(string prefix)
    {
        this.prefix = prefix;
    }

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