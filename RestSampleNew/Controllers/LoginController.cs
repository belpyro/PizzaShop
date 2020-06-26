﻿using Microsoft.Owin.Security;
using RestSample.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PizzaShop.Web.Controllers
{
    public class LoginController : ApiController
    {
        private readonly IUserService userService;

        public LoginController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet, Route("external/google")]
        public async Task<IHttpActionResult> GoogleLogin()
        {
            var provider = Request.GetOwinContext().Authentication;
            var loginInfo = await provider.GetExternalLoginInfoAsync();

            if (loginInfo == null) return BadRequest();

            await userService.RegisterExternalUser(loginInfo);
            return Ok();
        }

    }
}
