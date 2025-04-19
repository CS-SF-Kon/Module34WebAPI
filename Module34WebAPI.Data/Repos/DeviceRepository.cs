using Microsoft.EntityFrameworkCore;
using Module34WebAPI.Data.Models;
using Module34WebAPI.Data.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module34WebAPI.Data.Repos;

public class DeviceRepository : IDeviceRepository
{
    private readonly Module34WebAPIContext _context;

    public DeviceRepository(Module34WebAPIContext context)
    {
        _context = context;
    }

    public async Task<Device[]> GetDevices()
    {
        return await _context.Devices
            .Include(d => d.Room)
            .ToArrayAsync();
    }

    public async Task<Device> GetDeviceByName(string name)
    {
        return await _context.Devices
            .Include(d => d.Room)
            .Where(d => d.Name == name).FirstOrDefaultAsync();
    }

    public async Task<Device> GetDeviceById(Guid id)
    {
        return await _context.Devices
            .Include(d => d.Room)
            .Where(d => d.Id == id).FirstOrDefaultAsync();
    }

    public async Task SaveDevice(Device device, Room room)
    {
        device.RoomId = room.Id;
        device.Room = room;

        var entry = _context.Entry(device);
        if (entry.State == EntityState.Detached)
            await _context.Devices.AddAsync(device);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateDevice(Device device, Room room, UpdateDeviceQuery query)
    {
        device.RoomId = room.Id;
        device.Room = room;

        if (!string.IsNullOrEmpty(query.NewName))
            device.Name = query.NewName;
        if (!string.IsNullOrEmpty(query.NewSerial))
            device.SerialNumber = query.NewSerial;

        var entry = _context.Entry(device);
        if (entry.State == EntityState.Detached)
            _context.Devices.Update(device);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteDevice(Device device)
    {
        _context.Devices.Remove(device);
        await _context.SaveChangesAsync();
    }
}
