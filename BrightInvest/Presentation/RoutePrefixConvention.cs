using Microsoft.AspNetCore.Mvc.ApplicationModels;

public class RoutePrefixConvention : IApplicationModelConvention
{
	private readonly string _prefix;

	public RoutePrefixConvention(string prefix)
	{
		_prefix = prefix;
	}

	public void Apply(ApplicationModel application)
	{
		// Add the prefix to all controllers
		foreach (var controller in application.Controllers)
		{
			if (controller.Selectors.Any(selector => selector.AttributeRouteModel != null))
			{
				// If controller already has a route, append the prefix
				foreach (var selector in controller.Selectors)
				{
					selector.AttributeRouteModel.Template = _prefix + "/" + selector.AttributeRouteModel.Template;
				}
			}
			else
			{
				// If no route is defined, set the prefix as the controller's route
				controller.Selectors.Add(new SelectorModel
				{
					AttributeRouteModel = new AttributeRouteModel
					{
						Template = _prefix
					}
				});
			}
		}
	}
}
