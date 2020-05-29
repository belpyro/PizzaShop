using FluentValidation;
using RestSample.Data.Contexts;
using RestSample.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSample.Logic.Validators
{
    public class PizzaDtoValidator : AbstractValidator<PizzaDto>
    {
        private readonly PizzaShopContext _context;

        public PizzaDtoValidator(PizzaShopContext context)
        {
            this._context = context;

            RuleSet("PreValidation", () =>
            {
                RuleFor(x => x.Name).NotNull().MinimumLength(5)
                    .WithMessage("Field Name is invalid");
            });

            RuleSet("PostValidation", () =>
            {
                RuleFor(x => x.Name).Must(CheckDuplicate);
            });
        }

        private bool CheckDuplicate(string name)
        {
            return !_context.Pizzas.AsNoTracking().Any(x => x.Name == name);
        }
    }
}
