using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Module34WebAPI.Configuration;
using Module34WebAPI.Contracts.Models.Devices;
using Module34WebAPI.Data.Models;
using Module34WebAPI.Data.Repos;
using Module34WebAPI.Data.Queries;

namespace Module34WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DevicesController : ControllerBase
{
    private IDeviceRepository _deviceRepository;
    private IRoomRepository _roomRepository;
    private IMapper _mapper;

    public DevicesController(IMapper mapper, IRoomRepository rooms, IDeviceRepository devices)
    {
        _deviceRepository = devices;
        _mapper = mapper;
        _roomRepository = rooms;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetDevices()
    {
        var devices = await _deviceRepository.GetDevices();

        var resp = new GetDevicesResponse
        {
            DeviceAmount = devices.Length,
            Devices = _mapper.Map<Device[], DeviceView[]>(devices)
        };
        return StatusCode(200, resp);
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Add(AddDeviceRequest request)
    {
        var room = await _roomRepository.GetRoomByName(request.Location);
        if (room == null)
            return StatusCode(400, $"Room {request.Location} not connected yet");

        var device = await _deviceRepository.GetDeviceByName(request.Name);
        if (device != null)
            return StatusCode(400, $"Device {request.Name} already exists");

        var newDevice = _mapper.Map<AddDeviceRequest, Device>(request);
        await _deviceRepository.SaveDevice(newDevice, room);

        return StatusCode(200, $"Device {request.Name} successfully added. ID - {newDevice.Id}");
    }

    [HttpPatch]
    [Route("{id}")]
    public async Task<IActionResult> Edit([FromRoute] Guid id, [FromBody] EditDeviceRrequest request)
    {
        var room = await _roomRepository.GetRoomByName(request.NewRoom);
        if (room == null )
            return StatusCode(400, $"Room {request.NewRoom} not connected yet");

        var device = await _deviceRepository.GetDeviceById(id);
        if (device == null)
            return StatusCode(400, $"Device {id} does not exists");

        var sameName = await _deviceRepository.GetDeviceByName(request.NewName);
        if (sameName != null)
            return StatusCode(400, $"Device named {request.NewName} already exists");

        await _deviceRepository.UpdateDevice(device, room, new UpdateDeviceQuery(request.NewName, request.NewSerial));

        return StatusCode(200, $"Device {device.Name} with serial {device.SerialNumber} in {device.Room.Name} updated");
    }
}
