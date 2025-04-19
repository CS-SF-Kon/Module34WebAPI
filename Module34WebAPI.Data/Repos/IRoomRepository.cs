using Module34WebAPI.Data.Models;

namespace Module34WebAPI.Data.Repos;

public interface IRoomRepository
{
    Task<Room> GetRoomByName(string roomName);
    Task AddRoom(Room room);
}
