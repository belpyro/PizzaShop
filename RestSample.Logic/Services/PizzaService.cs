using AutoMapper;
using FluentValidation;
using RestSample.Data.Contexts;
using RestSample.Data.Models;
using RestSample.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RestSample.Logic.Services
{
    internal class PizzaService : IPizzaService
    {
        private static List<PizzaDto> _pizzas = PizzaFaker.Generate();
        private readonly PizzaShopContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<PizzaDto> _validator;

        public PizzaService(PizzaShopContext context, IMapper mapper, IValidator<PizzaDto> validator)
        {
            this._context = context;
            this._mapper = mapper;
            this._validator = validator;
        }

        public IEnumerable<PizzaDto> GetAll()
        {
            //return _context.Pizzas.ProjectToArray<PizzaDto>(_mapper.ConfigurationProvider);
            var models = _context.Pizzas.AsNoTracking().Include("Ingredients").ToArray();
            return _mapper.Map<IEnumerable<PizzaDto>>(models);
        }

        public PizzaDto GetById(int id)
        {
            var dbModel = new PizzaDb { Id = id };
            _context.Pizzas.Attach(dbModel);
            var entry = _context.Entry(dbModel);

            entry.Collection(x => x.Ingredients).Load();
            return _mapper.Map<PizzaDto>(entry.Entity);
            //return _context.Pizzas.Where(x => x.Id == id).ProjectToSingleOrDefault<PizzaDto>(_mapper.ConfigurationProvider);
        }

        public PizzaDto Add(PizzaDto model)
        {
            // validation
            _validator.ValidateAndThrow(model, "PostValidation");

            var dbModel = _mapper.Map<PizzaDb>(model);

            _context.Pizzas.Add(dbModel);
            _context.SaveChanges();

            model.Id = dbModel.Id;
            return model;
        }

        public void Update(PizzaDto model)
        {
            //var dbModel = _context.Pizzas.Find(model.Id); //SELECT
            //dbModel.Name = model.Name;
            //dbModel.Price = model.Price;
            var dbModel = _mapper.Map<PizzaDb>(model);
            _context.Pizzas.Attach(dbModel);
            var entry = _context.Entry(dbModel);
            // global state
            //entry.State = System.Data.Entity.EntityState.Modified;
            entry.Property(x => x.Name).IsModified = true;
            entry.Property(x => x.Price).IsModified = true;

            _context.SaveChanges(); //UPDATE
        }

        public void Delete(int id)
        {
            var dbModel = _context.Pizzas.Find(id); //SELECT
            _context.Pizzas.Remove(dbModel);
            _context.SaveChanges(); //DELETE
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                _context.Dispose();
                GC.SuppressFinalize(this);
                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~PizzaService()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}
