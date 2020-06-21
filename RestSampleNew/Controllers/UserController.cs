using RestSample.Logic.Models;
using RestSample.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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



    }
}
