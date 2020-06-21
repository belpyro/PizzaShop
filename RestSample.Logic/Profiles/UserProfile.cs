using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using RestSample.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSample.Logic.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<IdentityUser, UserDto>();
        }
    }
}
