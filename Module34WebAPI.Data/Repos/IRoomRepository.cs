using Module34WebAPI.Data.Models;
using Module34WebAPI.Data.Queries;


namespace Module34WebAPI.Data.Repos;

public interface IRoomRepository
{
    Task<Room> GetRoomByName(string roomName);
    Task<Room> GetRoomById(Guid id);
    Task AddRoom(Room room);
    Task<Room[]> GetAllRooms(); // добавлено в рамках задания 34.8.3
    Task UpdateRoom(Room room, UpdateRoomQuery query); // добавлено в рамках задания 34.8.3
    //Task UpdateRoom(Task<Room> room, UpdateRoomQuery updateRoomQuery);
}
