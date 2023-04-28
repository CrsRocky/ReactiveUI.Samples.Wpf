using AutoMapper;
using ReactiveUI.Samples.Wpf.Dtos;
using ReactiveUI.Samples.Wpf.Models;

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