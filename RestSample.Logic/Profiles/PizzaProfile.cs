using AutoMapper;
using RestSample.Data.Models;
using RestSample.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSample.Logic.Profiles
{
    class PizzaProfile : Profile
    {
        public PizzaProfile()
        {
            CreateMap<PizzaDb, PizzaDto>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name + " Pizza"))
                .ReverseMap();
        }
        // Mapster
    }

    class IngredientProfile : Profile
    {
        public IngredientProfile()
        {
            CreateMap<IngredientDb, IngredientDto>()
                .ReverseMap();
        }
    }
}
