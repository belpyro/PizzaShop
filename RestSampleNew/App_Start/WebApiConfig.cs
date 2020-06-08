using Elmah.Contrib.WebApi;
using FluentValidation.WebApi;
using RestSampleNew.Helpers;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Routing;

namespace RestSampleNew
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Elmah
            // Web API configuration and services
            var resolver = new DefaultInlineConstraintResolver();
            resolver.ConstraintMap.Add("pizza-name", typeof(PizzaNameConstraint));

            // Web API routes
            config.MapHttpAttributeRoutes(resolver);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Services.Replace(typeof(IExceptionLogger), new ElmahExceptionLogger());

            FluentValidationModelValidatorProvider.Configure(config, opt =>
            {
                opt.ValidatorFactory = new CustomValidatorFactory(config.DependencyResolver);
            });
        }
    }
}
