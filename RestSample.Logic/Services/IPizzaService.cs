using CSharpFunctionalExtensions;
using RestSample.Logic.Models;
using System;
using System.Collections.Generic;

namespace RestSample.Logic.Services
{
    public interface IPizzaService: IDisposable
    {
        Result<IEnumerable<PizzaDto>> GetAll();

        Result<Maybe<PizzaDto>> GetById(int id);

        Result<PizzaDto> Add(PizzaDto model);

        void Update(PizzaDto model);

        void Delete(int id);
    }
}