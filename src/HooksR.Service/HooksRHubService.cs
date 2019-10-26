using HooksR.DTO;
using HooksR.Entities;
using HooksR.Hubs;
using HooksR.Options.Utils;
using HooksR.Service.Contracts;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace HooksR.Service
{
  public class HooksRHubService : IHubService
  {
    private IHubContext<HookRHub> _hubContext;

    public HooksRHubService(IHubContext<HookRHub> hubContext)
    {
      _hubContext = hubContext;
    }
    public Task SendAsSync(string receive, UIPushEvent hooksREvent)
    {
      return _hubContext.Clients.All.SendAsync(receive, hooksREvent);
    }
  }
}
