//[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(RestSampleNew.App_Start.NinjectWebCommon), "Start")]
//[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(RestSampleNew.App_Start.NinjectWebCommon), "Stop")]

namespace RestSampleNew.App_Start
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using RestSample.Logic;
    using RestSample.Logic.Services;
    using Serilog;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var logger = new LoggerConfiguration()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.File(Path.Combine(path,"log.txt"))
                .Enrich.WithHttpRequestType()
                .Enrich.WithWebApiControllerName()
                .Enrich.WithWebApiActionName()
#if QA || DEBUG
                .MinimumLevel.Verbose()
#elif RELEASE
                 .MinimumLevel.Warning()
#endif
                .CreateLogger();

            kernel.Bind<ILogger>().ToConstant(logger);
            kernel.Load(new LogicDIModule());
        }
    }
}