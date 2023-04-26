using AutoMapper;
using ReactiveUI.Samples.Wpf.Dtos;
using ReactiveUI.Samples.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUI.Samples.Wpf.Services
{
    public class AutoMapperServices : Profile
    {
        public AutoMapperServices()
        {
            CreateMap<PeopleModel, PeopleDto>().ReverseMap();
        }
    }
}
