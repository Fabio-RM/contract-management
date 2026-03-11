using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;

namespace WebApi.Conventions;

public class RoutePrefixConvention : IApplicationModelConvention
{
    private readonly AttributeRouteModel _routePrefix;

    public RoutePrefixConvention(IRouteTemplateProvider route)
    {
        _routePrefix = new AttributeRouteModel(route);
    }

    public void Apply(ApplicationModel application)
    {
        foreach (var controller in application.Controllers)
        {
            var selectors = controller.Selectors;
            if (selectors.Any(selector => selector.AttributeRouteModel != null))
            {
                // Combine the prefix with existing attribute routes
                foreach (var selector in selectors.Where(selector => selector.AttributeRouteModel != null))
                {
                    selector.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(_routePrefix, selector.AttributeRouteModel);
                }
            }
            else
            {
                // Add the prefix to controllers without an existing attribute route
                foreach (var selector in selectors)
                {
                    selector.AttributeRouteModel = _routePrefix;
                }
            }
        }
    }
}
