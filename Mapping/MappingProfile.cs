using System.Linq;
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
            CreateMap<Feature, FeatureResource>();

            CreateMap<VehicleResource, Vehicle>()
            .ForMember(v => v.IsRegistered, opt => opt.MapFrom(vr => vr.IsRegistered))
            .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
            .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
            .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
            .ForMember(v => v.Features, opt => opt.MapFrom(vr => vr.Features.Select(id => new VehicleFeature() { FeatureId = id })));
        }
    }
}