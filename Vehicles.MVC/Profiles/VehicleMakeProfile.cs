using AutoMapper;
using Vehicles.MVC.ViewModels;
using Vehicles.Service.Models;

namespace Vehicles.MVC.Profiles;

public class VehicleMakeProfile : Profile
{
    public VehicleMakeProfile()
    {
        CreateMap<VehicleModel, VehicleModelViewModel>();
        CreateMap<VehicleMake, VehicleMakeViewModel>();
        CreateMap<VehicleMake, VehicleMakeViewModel>()
            .ForMember(dest => dest.VehicleModels, opt => opt.MapFrom(src => src.VehicleModels));
        CreateMap<VehicleMakeViewModel, VehicleMake>();

    }
}