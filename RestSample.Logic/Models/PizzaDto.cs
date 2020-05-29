using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RestSample.Logic.Models
{
     public class PizzaDto : IValidatableObject //DTO Data Transfer Object
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public IEnumerable<IngredientDto> Ingredients { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var result = new List<ValidationResult>();

            if(Name.Equals("Pepperoni", System.StringComparison.OrdinalIgnoreCase))
            {
                result.Add(new ValidationResult("Name cannot be called as 'Peperoni'"));
                return result.AsReadOnly();
            }

            // validation logic

            return Enumerable.Empty<ValidationResult>();
        }
    }
}
