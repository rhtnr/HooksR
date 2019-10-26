using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HooksR.Hubs
{
  public class HookRHub:Hub
  {
    private readonly ILogger<HookRHub> _logger;

    public HookRHub(ILogger<HookRHub> logger)
    {
      _logger = logger;
    }
    public override Task OnConnectedAsync()
    {
      _logger.LogDebug("A client has connected");
      return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
      _logger.LogDebug("A client has disconnected");
      return base.OnDisconnectedAsync(exception);
    }
  }
}
