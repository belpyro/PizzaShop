using AutoMapper;
using CSharpFunctionalExtensions;
using FluentValidation;
using JetBrains.Annotations;
using RestSample.Data.Contexts;
using RestSample.Data.Models;
using RestSample.Logic.Models;
using RestSample.Logic.Validators;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;

namespace RestSample.Logic.Services
{
    public class PizzaService : IPizzaService
    {
        private readonly PizzaShopContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public PizzaService([NotNull]PizzaShopContext context,
            [NotNull]IMapper mapper,
            [NotNull] ILogger logger)
        {
            this._context = context;
            this._mapper = mapper;
            this._logger = logger;
        }

        public Result<IEnumerable<PizzaDto>> GetAll()
        {
            //return _context.Pizzas.ProjectToArray<PizzaDto>(_mapper.ConfigurationProvider);
            try
            {
                _logger.Warning("Get all pizzas requested by anonymous");
                var models = _context.Pizzas.AsNoTracking().Include(x => x.Ingredients).ToArray();
                return Result.Success(_mapper.Map<IEnumerable<PizzaDto>>(models));
            }
            catch (SqlException ex)
            {
                _logger.Error("Connection to db is failed", ex);
                return Result.Failure<IEnumerable<PizzaDto>>(ex.Message);
            }
        }

        public Result<Maybe<PizzaDto>> GetById(int id)
        {
            //var dbModel = new PizzaDb { Id = id };
            //_context.Pizzas.Attach(dbModel);
            //var entry = _context.Entry(dbModel);

            //entry.Collection(x => x.Ingredients).Load();
            //return _mapper.Map<PizzaDto>(entry.Entity);
            try
            {
                Maybe<PizzaDto> pizza = _context.Pizzas.Where(x => x.Id == id).ProjectToSingleOrDefault<PizzaDto>(_mapper.ConfigurationProvider);
                return Result.Success(pizza);
            }
            catch (SqlException ex)
            {
                return Result.Failure<Maybe<PizzaDto>>(ex.Message);
            }

        }

        public Result<PizzaDto> Add(PizzaDto model)
        {
            try
            {
                var dbModel = _mapper.Map<PizzaDb>(model);

                _context.Pizzas.Add(dbModel);
                _context.SaveChanges();

                model.Id = dbModel.Id;
                return Result.Success(model);
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure<PizzaDto>(ex.Message);
            }
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


        public Result<IEnumerable<PizzaDto>> GetByName(string name)
        {
            try
            {
                return _context.Pizzas.AsNoTracking().Where(p => p.Name.Contains(name)).ProjectToArray<PizzaDto>(_mapper.ConfigurationProvider);
            }
            catch (SqlException ex)
            {
                _logger.Error(ex, "Cannot connect to db");
                return Result.Failure<IEnumerable<PizzaDto>>("Cannot connect to db");
            }
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
