﻿using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PizzaShop.Web.Middleware
{
    public class GoogleAuthMiddleware : OwinMiddleware
    {
        public GoogleAuthMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override Task Invoke(IOwinContext context)
        {
            context.Authentication.Challenge(new Microsoft.Owin.Security.AuthenticationProperties
            {
                RedirectUri = "/external/google"
            }, "MyGoogle");
            return Task.CompletedTask;
        }
    }
}