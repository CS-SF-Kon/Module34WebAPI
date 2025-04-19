﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module34WebAPI.Contracts.Models.Rooms;
using Module34WebAPI.Data.Repos;
using Module34WebAPI.Data.Models;
using Module34WebAPI.Data.Queries;

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

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Edit([FromRoute] Guid id, [FromBody] EditRoomRequest request) // добавить метод  изменения команты в контроллер
    {
        var room = await _roomRepository.GetRoomById(id);
        if (room == null)
            return StatusCode(400, $"Room with id {id} does not exists");

        var sameName = await _roomRepository.GetRoomByName(request.NewName);
        if (sameName != null)
            return StatusCode(400, $"Room named {request.NewName} already exists");

        await _roomRepository.UpdateRoom(room, new UpdateRoomQuery(request.NewName, request.NewVoltage, request.NewGasConnected));

        return StatusCode(200, $"Room {id} were updated"); // интересно, что в таблице устройств названия комнат не меняются, надо доработать - добавить метод поиска устройств по id редактируемой комнаты и пр
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetRooms()
    {
        var rooms = await _roomRepository.GetAllRooms();

        var resp = new GetRoomsResponse
        {
            RoomAmount = rooms.Length,
            Rooms = _mapper.Map<Room[], RoomView[]>(rooms)
        };

        return StatusCode(200, resp);
    }
}
