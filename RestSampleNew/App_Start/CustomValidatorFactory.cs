using FluentValidation;
using Ninject;
using System;
using System.Web.Http.Dependencies;

namespace RestSampleNew
{
    public class CustomValidatorFactory : IValidatorFactory
    {
        private IDependencyResolver _dependencyResolver;
        private IKernel kernel;

        public CustomValidatorFactory(IDependencyResolver dependencyResolver)
        {
            this._dependencyResolver = dependencyResolver;
        }

        public CustomValidatorFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public IValidator<T> GetValidator<T>()
        {
           return kernel.GetService(typeof(T)) as IValidator<T>;
        }

        public IValidator GetValidator(Type type)
        {
             return kernel.GetService(type) as IValidator;
        }
    }
}
