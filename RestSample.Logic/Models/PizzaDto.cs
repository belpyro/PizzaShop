using FluentValidation.Attributes;
using RestSample.Logic.Validators;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RestSample.Logic.Models
{
    [Validator(typeof(PizzaDtoValidator))]
     public class PizzaDto//DTO Data Transfer Object
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public IEnumerable<IngredientDto> Ingredients { get; set; }
    }
}
