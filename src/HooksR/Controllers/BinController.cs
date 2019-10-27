using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HooksR.DTO;
using HooksR.Hubs;
using HooksR.Options.Utils;
using HooksR.Service.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HooksR.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class BinController : ControllerBase
  {
    private readonly IHubContext<HookRHub> _hubContext;
    private readonly IHubService _hubService;
    private readonly IMapper _mapper;

    public BinController(IHubContext<HookRHub> hubContext, IHubService hubService, IMapper mapper)
    {
      _hubContext = hubContext;
      _hubService = hubService;
      _mapper = mapper;
    }
    

    [HttpGet]
    public IEnumerable<string> Get()
    {
      return new string[] { "value1", "value2" };
    }

    
    [HttpGet("{id}", Name = "Get")]
    public string Get(int id)
    {
      return "value";
    }

   
    [HttpPost]
    public IActionResult Post()
    {
      var uiEvent = _mapper.Map<UIPushEvent>(HttpContext);
      _hubService.SendAsSync(Methods.UIReceive, uiEvent);
      return Ok();
    }


    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
