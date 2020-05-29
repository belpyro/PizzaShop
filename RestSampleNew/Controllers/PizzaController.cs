﻿using FluentValidation;
using FluentValidation.WebApi;
using RestSample.Logic.Models;
using RestSample.Logic.Services;
using System.Net;
using System.Web.Http;

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
        public IHttpActionResult GetAll()
        {
            return Ok(_pizzaService.GetAll());
        }


        // api/pizzas/5
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById(int id)
        {
            var pizza = _pizzaService.GetById(id);
            return pizza == null ? (IHttpActionResult)NotFound() : Ok(pizza);
        }

        //Swagger - OpenApi
        //SwaggerUI <- OpenAPI

        //INSERT
        [HttpPost]
        [Route("")]
        public IHttpActionResult Add([CustomizeValidator(RuleSet = "PreValidation")][FromBody]PizzaDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                model = _pizzaService.Add(model);
                return Created($"/pizzas/{model.Id}", model);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //UPDATE
        [HttpPut]
        [Route("{id}")]
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
