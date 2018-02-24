using AutoMapper;
using CarsCore.Controllers.Resources;
using CarsCore.Models;

namespace CarsCore.Mapping
{
    public class MappingProfile : Profile
    {
         public MappingProfile()
         {
             CreateMap<Make, MakeResource>();
             CreateMap<Model, ModelResource>();
         }
    }
}