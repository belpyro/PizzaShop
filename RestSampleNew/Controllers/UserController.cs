﻿using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using RestSample.Logic.Models;
using RestSample.Logic.Services;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestSampleNew.Controllers
{
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }


        [HttpPost, Route("register")]
        public async Task<IHttpActionResult> Register([FromBody]NewUserDto model)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid model");

            var result = await _userService.Register(model);

            return result.IsSuccess ? StatusCode(HttpStatusCode.NoContent) : StatusCode(HttpStatusCode.InternalServerError);
        }


        [HttpPost, Route("login")]
        public async Task<IHttpActionResult> Login([FromBody]LoginDto model)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid model");

            var result = await _userService.GetUser(model.UserName, model.Password);
            if (result.HasNoValue) return Unauthorized();

            //ClaimsPrincipal
            //ClaimsIdentity
            var identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, result.Value.Id));
            identity.AddClaim(new Claim(ClaimTypes.Name, result.Value.UserName));

            var provider = Request.GetOwinContext().Authentication;

            // use for AspNet.Identity integration with OWIN pipeline
            //var manager = Request.GetOwinContext().Get<UserManager<IdentityUser>>();
            //var idn = await manager.CreateIdentityAsync(new IdentityUser { }, DefaultAuthenticationTypes.ApplicationCookie);

            provider.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            provider.SignIn(new AuthenticationProperties { IsPersistent = true }, identity);

            return Ok();
        }
    }
}
