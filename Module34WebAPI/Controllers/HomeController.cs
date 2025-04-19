using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Module34WebAPI.Configuration;
using Module34WebAPI.Contracts.Models.Home;
using System.Text;

namespace Module34WebAPI.Controllers;

[Route("api/[controller]")] // "api/" добавлено по дефолту, не стал убирать
[ApiController]
public class HomeController : ControllerBase
{
    private IOptions<HomeOptions> _options;
    private IMapper _mapper;

    public HomeController(IOptions<HomeOptions> options, IMapper mapper)
    {
        _options = options;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("info")]
    public IActionResult Info()
    {
        var infoResponse = _mapper.Map<HomeOptions, InfoResponse>(_options.Value);

        return StatusCode(200, infoResponse);
    }
}
