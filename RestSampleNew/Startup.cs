using System.IO;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;
using System.Web.Http.Routing;
using Elmah.Contrib.WebApi;
using FluentValidation.WebApi;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using NSwag.AspNet.Owin;
using Owin;
using RestSample.Logic;
using RestSampleNew.Controllers;
using RestSampleNew.Helpers;
using Serilog;

[assembly: OwinStartup(typeof(RestSampleNew.Startup))]

namespace RestSampleNew
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            var resolver = new DefaultInlineConstraintResolver();
            resolver.ConstraintMap.Add("pizza-name", typeof(PizzaNameConstraint));

            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes(resolver);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Services.Replace(typeof(IExceptionLogger), new ElmahExceptionLogger());
            var provider = new SimpleModelBinderProvider(typeof(Filter), new FilterBinder());

            config.Services.Insert(typeof(ModelBinderProvider), 0, provider);

            var kernel = new StandardKernel();
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var logger = new LoggerConfiguration()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.File(Path.Combine(path, "log.txt"))
                .Enrich.WithHttpRequestType()
                .Enrich.WithWebApiControllerName()
                .Enrich.WithWebApiActionName()
#if QA || DEBUG
                .MinimumLevel.Verbose()
#elif RELEASE
                 .MinimumLevel.Warning()
#endif
                .CreateLogger();

            config.EnsureInitialized();

            kernel.Bind<ILogger>().ToConstant(logger);
            kernel.Load(new LogicDIModule());

            FluentValidationModelValidatorProvider.Configure(config, opt =>
            {
                opt.ValidatorFactory = new CustomValidatorFactory(kernel);
            });

            //app.CreatePerOwinContext<PizzaShopDbContext>(() => { });
            //app.CreatePerOwinContext<UserManager<IdentityUser>(() => new UserManager<IdentityUser>(new UserStore<IdentityUser>(new System.Data.Entity.DbContext())));

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            });

            app.UseSwagger(typeof(Startup).Assembly).UseSwaggerUi3().UseNinjectMiddleware(() => kernel).UseNinjectWebApi(config);
        }
    }
}
