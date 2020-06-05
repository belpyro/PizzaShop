using Castle.DynamicProxy;
using CSharpFunctionalExtensions;
using FluentValidation;
using Ninject;
using RestSample.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSample.Logic.Aspects
{
    public class ValidationInterceptor : IInterceptor
    {
        private readonly IKernel _kernel;

        public ValidationInterceptor(IKernel kernel)
        {
            _kernel = kernel;
        }

        public void Intercept(IInvocation invocation)
        {
            var arg = invocation.Arguments.OfType<PizzaDto>().FirstOrDefault();
            if (arg == null)
            {
                invocation.Proceed();
                return;
            }

            var validator = _kernel.Get<IValidator<PizzaDto>>();
            var validationResult = validator.Validate(arg, "PostValidation"); // contract
            if (!validationResult.IsValid)
            {
                invocation.ReturnValue = Result.Failure<PizzaDto>(validationResult.Errors.Select(x => x.ErrorMessage).First());
            }

            invocation.Proceed();
        }
    }
}
