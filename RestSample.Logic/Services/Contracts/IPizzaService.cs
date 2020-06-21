using CSharpFunctionalExtensions;
using RestSample.Logic.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestSample.Logic.Services
{
    public interface IPizzaService: IDisposable
    {
        Result<IEnumerable<PizzaDto>> GetAll();

        Task<Result<IEnumerable<PizzaDto>>> GetAllAsync();

        Result<Maybe<PizzaDto>> GetById(int id);

        Result<PizzaDto> Add(PizzaDto model);

        void Update(PizzaDto model);

        void Delete(int id);

        Result<IEnumerable<PizzaDto>> GetByName(string name);
    }
}