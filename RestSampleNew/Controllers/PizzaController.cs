using FluentValidation;
using FluentValidation.WebApi;
using RestSample.Logic.Models;
using RestSample.Logic.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace RestSampleNew.Controllers
{
    [RoutePrefix("api/pizzas")]
    public class PizzaController : ApiController
    {
        private readonly IPizzaService _pizzaService;

        public PizzaController(IPizzaService pizzaService)
        {
            this._pizzaService = pizzaService;
        }

        // SELECT
        //Get all
        //Get by id
        //Get by filter
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAllAsync()
        {
            var result = await _pizzaService.GetAllAsync();
            return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)StatusCode(HttpStatusCode.InternalServerError);
        }

        //// api/pizzas/all/5
        [HttpGet]
        [Route("all/{id:int}", Name = "GetPizzaById")]
        public IHttpActionResult GetById(int id) //1.. int.Max
        {
            var result = _pizzaService.GetById(id);
            if (result.IsFailure)
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
            return result.Value.HasNoValue ? (IHttpActionResult)NotFound() : Ok(result.Value.Value);
        }

        [HttpGet]
        [Route("all/filter", Name = "GetByFilter")]
        public IHttpActionResult GetByFilter([ModelBinder(typeof(FilterBinder))]Filter filter) //1.. int.Max
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpGet]
        [Route("names/{*demo}")]
        public IHttpActionResult GetAllByWildCard(string demo)
        {
            var result = _pizzaService.GetAll();
            return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)StatusCode(HttpStatusCode.InternalServerError);
        }

        // names/highest
        // names/for/lowest/cost

        [HttpGet]
        [Route("names/{rating}")]
        public IHttpActionResult GetAllByRating(string rating)
        {
            var result = _pizzaService.GetAll();
            return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)StatusCode(HttpStatusCode.InternalServerError);
        }



        [HttpGet, Route("{name:pizza-name}")]
        public IHttpActionResult GetByName(string name)
        {
            var result = _pizzaService.GetByName(name);
            return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)StatusCode(HttpStatusCode.InternalServerError);
        }

        //Swagger - OpenApi
        //SwaggerUI <- OpenAPI

        //INSERT
        [HttpPost]
        [Route("")]
        public IHttpActionResult Add([CustomizeValidator(RuleSet = "PreValidation")]PizzaDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _pizzaService.Add(model);
            var url = Url.Link("GetPizzaById", new { id = result.Value.Id });

            return result.IsSuccess ? Created(url, result.Value) : (IHttpActionResult)BadRequest(result.Error);

        }

        //UPDATE
        [HttpPut]
        [Route("{id:int:min(1)}")]
        public IHttpActionResult Update(int id, [FromBody]PizzaDto model)
        {
            _pizzaService.Update(model);
            return StatusCode(HttpStatusCode.NoContent);
        }

        //DELETE
        [HttpDelete]
        [Route("{id:int:min(1)}")]
        public IHttpActionResult Delete(int id)
        {
            // delete
            _pizzaService.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
