using AutoMapper;
using Vehicles.Model.DTO;
using Vehicles.Service.Models;

namespace Vehicles.Model.Common.Profiles;

public class VehiclesApiProfile : Profile
{
    public VehiclesApiProfile()
    {
        //CreateMap<VehicleModel, VehicleModelViewModel>();
        //CreateMap<VehicleModelViewModel, VehicleModel>();
        
        CreateMap<VehicleMake, VehicleMakeDto>();
        CreateMap<VehicleMake, VehicleMakeDto>()
            .ForMember(dest => dest.VehicleModels, opt => opt.MapFrom(src => src.VehicleModels));
        CreateMap<VehicleMakeDto, VehicleMake>();

    }
}