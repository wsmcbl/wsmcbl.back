using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace wsmcbl.src.utilities;

public class RoutePrefixConvention(string prefix) : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        foreach (var selector in controller.Selectors
                     .Where(p => p.AttributeRouteModel != null))
        {
            var routeModel = selector.AttributeRouteModel;
            if (routeModel != null)
            {
                routeModel.Template = $"{prefix}/{routeModel.Template}".Trim('/');
            }
        }

        foreach (var action in controller.Actions)
        {
            foreach (var selector in action.Selectors
                         .Where(s => s.AttributeRouteModel != null))
            {
                var routeModel = selector.AttributeRouteModel;
                if (routeModel != null)
                {
                    routeModel.Template = $"{prefix}/{routeModel.Template}".Trim('/');
                }
            }
        }
    }
}