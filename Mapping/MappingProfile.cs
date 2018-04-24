using System.Collections.Generic;
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
            CreateMap<Make, KeyValuePairResource>();
            CreateMap<Model, KeyValuePairResource>();
            CreateMap<Feature, KeyValuePairResource>();

            CreateMap<Vehicle, VehicleResource>()
            .ForMember(vr => vr.Contact, input => input.MapFrom(v => new ContactResource() { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone }))
            .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => new KeyValuePairResource() { Id = vf.Feature.Id, Name = vf.Feature.Name })))
            .ForMember(vr => vr.Make, opt => opt.MapFrom(v => v.Model.Make));

            CreateMap<Vehicle, SaveVehicleResource>()

            .ForMember(dest => dest.Contact, opt => opt.MapFrom(vr => new ContactResource() { Name = vr.ContactName, Email = vr.ContactEmail, Phone = vr.ContactPhone }))
            .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)))

            .ReverseMap()

            .ForMember(v => v.Id, opt => opt.Ignore())
            .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
            .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
            .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
            .ForMember(v => v.Features, opt => opt.Ignore())

            .AfterMap((vr, v) =>
            {
                var unselectedFeatures = v.Features.Where(f => !vr.Features.Contains(f.FeatureId)).ToList();

                foreach (var feature in unselectedFeatures)
                    v.Features.Remove(feature);

                var addedFeatures = vr.Features.Where(id => !v.Features.Any(f => f.FeatureId == id)).Select(id => new VehicleFeature() { FeatureId = id });

                foreach (var feature in addedFeatures)
                {
                    v.Features.Add(feature);
                }
            });
        }
    }
}