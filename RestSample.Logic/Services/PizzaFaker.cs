using Bogus;
using RestSample.Logic.Models;
using System.Collections.Generic;

namespace RestSample.Logic.Services
{
    internal static class PizzaFaker
    {
        private static Faker<PizzaDto> _faker;

        static PizzaFaker()
        {
            _faker = new Faker<PizzaDto>();
            _faker.RuleFor(x => x.Id, f => f.IndexFaker)
                .RuleFor(x => x.Name, f => f.Commerce.Product())
                .RuleFor(x => x.Price, f => f.Random.Int(10, 500));
        }

        internal static List<PizzaDto> Generate(int count = 100)
        {
            return _faker.Generate(count);
        }
    }
}
