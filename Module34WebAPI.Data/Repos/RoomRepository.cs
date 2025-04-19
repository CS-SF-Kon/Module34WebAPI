using Microsoft.EntityFrameworkCore;
using Module34WebAPI.Data.Models;

namespace Module34WebAPI.Data.Repos;

public class RoomRepository : IRoomRepository
{
    private readonly Module34WebAPIContext _context;

    public RoomRepository(Module34WebAPIContext context)
    {
        _context = context;
    }

    public async Task<Room> GetRoomByName(string name)
    {
        return await _context.Rooms.Where(r => r.Name == name).FirstOrDefaultAsync();
    }

    public async Task AddRoom(Room room)
    {
        var entry = _context.Entry(room);
        if (entry.State == EntityState.Detached)
            await _context.Rooms.AddAsync(room);

        await _context.SaveChangesAsync();
    }
}
