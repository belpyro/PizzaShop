using FluentValidation;
using System;
using System.Web.Http.Dependencies;

namespace RestSampleNew
{
    public class CustomValidatorFactory : IValidatorFactory
    {
        private IDependencyResolver _dependencyResolver;

        public CustomValidatorFactory(IDependencyResolver dependencyResolver)
        {
            this._dependencyResolver = dependencyResolver;
        }

        public IValidator<T> GetValidator<T>()
        {
           return _dependencyResolver.GetService(typeof(T)) as IValidator<T>;
        }

        public IValidator GetValidator(Type type)
        {
             return _dependencyResolver.GetService(type) as IValidator;
        }
    }
}
