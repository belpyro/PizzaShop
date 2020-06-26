using AutoMapper;
using FluentValidation;
using Ninject.Modules;
using RestSample.Data.Contexts;
using RestSample.Logic.Models;
using RestSample.Logic.Profiles;
using RestSample.Logic.Services;
using RestSample.Logic.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Ninject;
using RestSample.Logic.Aspects;
using System.Threading;
using Serilog;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RestSample.Logic
{
    public class LogicDIModule : NinjectModule
    {
        public override void Load()
        {
            Mapper.Initialize(cfg => cfg.AddProfiles(typeof(PizzaProfile)));
            var mapper = Mapper.Configuration.CreateMapper();

            this.Bind<IMapper>().ToConstant(mapper);
            this.Bind<PizzaShopContext>().ToSelf();
            this.Bind<IValidator<PizzaDto>>().To<PizzaDtoValidator>();

            this.Bind<IPizzaService>().ToMethod(ctx =>
            {
                var service = new PizzaService(ctx.Kernel.Get<PizzaShopContext>(), ctx.Kernel.Get<IMapper>(), ctx.Kernel.Get<ILogger>());
                return new ProxyGenerator().CreateInterfaceProxyWithTarget<IPizzaService>(service, new ValidationInterceptor(ctx.Kernel));
            });// .....

            //register asp.net identity

            this.Bind<IUserStore<IdentityUser>>().ToMethod(ctx => new UserStore<IdentityUser>(ctx.Kernel.Get<PizzaShopContext>()));
            this.Bind<UserManager<IdentityUser>>().ToMethod(ctx =>
            {
                var manager = new UserManager<IdentityUser>(ctx.Kernel.Get<IUserStore<IdentityUser>>());
                manager.EmailService = new PizzaEmailService();
                manager.UserValidator = new UserValidator<IdentityUser>(manager)
                {
                    AllowOnlyAlphanumericUserNames = false,
                    RequireUniqueEmail = true
                };
                manager.PasswordValidator = new PasswordValidator()
                {
                    RequireDigit = false,
                    RequiredLength = 3,
                    RequireLowercase = false,
                    RequireNonLetterOrDigit = false,
                    RequireUppercase = false
                };

                manager.UserTokenProvider = new EmailTokenProvider<IdentityUser>();

                return manager;
            });

            this.Bind<IUserService>().To<UserService>();
        }
    }
}
