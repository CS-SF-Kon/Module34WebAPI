using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module34WebAPI.Contracts.Models.Rooms;
using Module34WebAPI.Data.Repos;
using Module34WebAPI.Data.Models;

namespace Module34WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoomsController : ControllerBase
{
    private IRoomRepository _roomRepository;
    private IMapper _mapper;

    public RoomsController(IRoomRepository roomRepository, IMapper mapper)
    {
        _roomRepository = roomRepository;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Add([FromBody] AddRoomRequest request)
    {
        var existongRoom = await _roomRepository.GetRoomByName(request.Name);
        if (existongRoom == null)
        {
            var newRoom = _mapper.Map<AddRoomRequest, Room>(request);
            await _roomRepository.AddRoom(newRoom);
            return StatusCode(201, $"Room {request.Name} successfully added");
        }

        return StatusCode(409, $"Room {request.Name} already exists");
    }
}
