using System.ComponentModel.DataAnnotations;

namespace RestSample.Logic.Models
{
    public sealed class ForbiddenNamesAttribute : ValidationAttribute
    {
        public ForbiddenNamesAttribute(string names) // Name1,Name2,....NameN
        {
            Names = names;
        }

        public string Names { get; set; }

        public override bool IsValid(object value)
        {
            return !Names.Contains((value as PizzaDto).Name);
        }
    }
}
