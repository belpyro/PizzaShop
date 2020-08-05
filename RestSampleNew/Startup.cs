using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;
using System.Web.Http.Routing;
using Elmah.Contrib.WebApi;
using FluentValidation.WebApi;
using IdentityServer3.AccessTokenValidation;
using IdentityServer3.AspNetIdentity;
using IdentityServer3.Core;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.InMemory;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Extensions;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using NSwag.AspNet.Owin;
using Owin;
using PizzaShop.Web.Middleware;
using PizzaShop.Web.Security;
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

            var provide = new CorsPolicyProvider();
            provide.PolicyResolver = ctx => Task.FromResult(new System.Web.Cors.CorsPolicy { AllowAnyHeader = true, AllowAnyMethod = true, AllowAnyOrigin = true });

            app.UseCors(new CorsOptions { PolicyProvider = provide });
            app.UseStaticFiles();
            app.UseSwagger(typeof(Startup).Assembly).UseSwaggerUi3(settings => settings.ServerUrl = "http://demovm:50698");

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
            {
                ClientId = "273410476984-el7qkveap4ss84963sbl5gr4qpkb6445.apps.googleusercontent.com",
                ClientSecret = "MJM3XIpsQHPhAEHi-nL9dBvs",
                AuthenticationType = "MyGoogle"
            });

            IdentityServerServiceFactory factory = new IdentityServerServiceFactory();

            factory.UseInMemoryScopes(StandardScopes.All.Append(
                new Scope() { Name = "api", DisplayName = "Api", Description = "Access to API", Type = ScopeType.Resource, Claims = new List<ScopeClaim> { new ScopeClaim("api-version", true) } }))
                .UseInMemoryClients(new[] { Clients.WebClient, Clients.PasswordClient });
            factory.UserService = new Registration<IdentityServer3.Core.Services.IUserService>(new AspNetIdentityUserService<IdentityUser, string>(kernel.Get<UserManager<IdentityUser>>()));

            app.UseIdentityServer(new IdentityServerOptions
            {
                EnableWelcomePage = true,
#if DEBUG
                RequireSsl = false,
#endif
                LoggingOptions = new LoggingOptions
                {
                    EnableHttpLogging = true,
                    EnableKatanaLogging = true,
                    EnableWebApiDiagnostics = true,
                    WebApiDiagnosticsIsVerbose = true
                },
                SiteName = "PizzaShop",
                Factory = factory,
                SigningCertificate = LoadCertificate(),
            }).UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "http://demovm:50698",
                ClientId = "PizzaWebClient",
                ClientSecret = "secret",
                RequireHttps = false,
                ValidationMode = ValidationMode.Local,
                IssuerName = "http://demovm:50698",
                SigningCertificate = LoadCertificate(),
                ValidAudiences = new[] { "http://demovm:50698/resources" }
            });



            app.UseNinjectMiddleware(() => kernel).UseNinjectWebApi(config);
        }

        private static X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\Config\idsrv3test.pfx"), "idsrv3test");
        }
    }
}
