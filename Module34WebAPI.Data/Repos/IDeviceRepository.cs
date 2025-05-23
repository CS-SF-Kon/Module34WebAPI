﻿using Module34WebAPI.Data.Models;
using Module34WebAPI.Data.Queries;

namespace Module34WebAPI.Data.Repos;

public interface IDeviceRepository
{
    Task<Device[]> GetDevices();
    Task<Device> GetDeviceByName(string name);
    Task<Device> GetDeviceById(Guid id);
    Task SaveDevice(Device device, Room room);
    Task UpdateDevice(Device device, Room room, UpdateDeviceQuery query);
    Task DeleteDevice(Device device);
}
