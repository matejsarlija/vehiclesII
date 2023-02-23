using AutoMapper;
using Vehicles.Model.DTO;
using Vehicles.Service.Models;

namespace Vehicles.Model.Common.Profiles;

public class VehiclesApiProfile : Profile
{
    public VehiclesApiProfile()
    { 
        CreateMap<VehicleMake, VehicleMakeDto>()
            .ForMember(dest => dest.VehicleModels, opt => opt.MapFrom(src => src.VehicleModels));
        CreateMap<VehicleMakeDto, VehicleMake>();

        CreateMap<VehicleModel, VehicleModelDto>();
        CreateMap<VehicleModelDto, VehicleModel>();

    }
}