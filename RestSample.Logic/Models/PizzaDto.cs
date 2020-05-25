using System.Collections.Generic;

namespace RestSample.Logic.Models
{
    public class PizzaDto //DTO Data Transfer Object
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public IEnumerable<IngredientDto> Ingredients { get; set; }
    }
}
