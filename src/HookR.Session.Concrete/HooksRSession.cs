using HooksR.Session.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HooksR.Session.Concrete
{
  public class HooksRSession : IHooksRSession
  {
    public Task SignInAsync(IHttpContextAccessor httpContextAccessor)
    {
      var claims = new List<Claim>
      {
        new Claim(ClaimTypes.Hash, Guid.NewGuid().ToString())
      };

      var claimsIdentity = new ClaimsIdentity(
          claims, CookieAuthenticationDefaults.AuthenticationScheme);

      var authProperties = new Microsoft.AspNetCore.Authentication.AuthenticationProperties
      {


        IsPersistent = true,
        ExpiresUtc = DateTimeOffset.UtcNow.AddDays(100)

        //IssuedUtc = <DateTimeOffset>,
        // The time at which the authentication ticket was issued.

        //RedirectUri = <string>
      };
      return httpContextAccessor.HttpContext.SignInAsync(
                      CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties); 
    }

    public Task SignOutAsync(IHttpContextAccessor httpContextAccessor)
    {
      return httpContextAccessor.HttpContext.SignOutAsync(
          CookieAuthenticationDefaults.AuthenticationScheme);
    }
  }
}
