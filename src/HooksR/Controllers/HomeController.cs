using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HooksR.Models;
using Microsoft.AspNetCore.Authentication;
using HooksR.Session.Contracts;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using HooksR.Entities;

namespace HooksR.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly IHooksRSession _session;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HomeController(ILogger<HomeController> logger, IHooksRSession session, IHttpContextAccessor httpContextAccessor)
    {
      _logger = logger;
      _httpContextAccessor = httpContextAccessor;
      _session = session;
    }

    public async Task<IActionResult> IndexAsync()
    {
      User user = new User();
      if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
      {
        await _session.SignInAsync(_httpContextAccessor);
        user.Hash = "Not logged in!";
        return RedirectToAction();
      }
      else
      {

        var hash = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(d => d.Type == ClaimTypes.Hash).ToString();
        
        user.Hash = hash;
        ViewBag.User = user;
      }
      ViewBag.User = user;
      return View();
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
