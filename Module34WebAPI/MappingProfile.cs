using AutoMapper;
using Module34WebAPI.Configuration;
using Module34WebAPI.Contracts.Models.Devices;
using Module34WebAPI.Contracts.Models.Home;
using Module34WebAPI.Contracts.Models.Rooms;
using Module34WebAPI.Data.Models;

namespace Module34WebAPI;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Address, AddressInfo>();
        CreateMap<HomeOptions, InfoResponse>()
            .ForMember(m => m.AddressInfo,
                opt => opt.MapFrom(src => src.Address));
        CreateMap<AddDeviceRequest, Device>()
            .ForMember(d => d.Location,
                opt => opt.MapFrom(r => r.Location));
        CreateMap<AddRoomRequest, Room>();
        CreateMap<Device, DeviceView>();
        CreateMap<Room, RoomView>(); // добавлено для задания 34.8.3
    }
}
