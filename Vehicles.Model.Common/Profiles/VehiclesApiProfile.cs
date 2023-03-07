using AutoMapper;
using Vehicles.DAL.Models;
using Vehicles.Model.DTO;

namespace Vehicles.Model.Common.Profiles;

public class VehiclesApiProfile : Profile
{
    public VehiclesApiProfile()
    {
        //CreateMap<VehicleMake, VehicleMakeDto>();
        CreateMap<VehicleMake, VehicleMakeDto>()
            .ForMember(dest => dest.VehicleModels, opt => opt.MapFrom(src => src.VehicleModels));
        CreateMap<VehicleMakeDto, VehicleMake>();

        CreateMap<VehicleModel, VehicleModelDto>();
        CreateMap<VehicleModelDto, VehicleModel>();

    }
}