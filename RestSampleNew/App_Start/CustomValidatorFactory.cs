using FluentValidation;
using Ninject;
using System;
using System.Web.Http.Dependencies;

namespace RestSampleNew
{
    public class CustomValidatorFactory : IValidatorFactory
    {
        private IKernel kernel;

        public CustomValidatorFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public IValidator<T> GetValidator<T>()
        {
           return kernel.TryGet(typeof(T)) as IValidator<T>;
        }

        public IValidator GetValidator(Type type)
        {
             return kernel.TryGet(type) as IValidator;
        }
    }
}
