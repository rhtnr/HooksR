using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace HooksR.Session.Contracts
{
  public interface IHooksRSession
  {
    Task SignInAsync(IHttpContextAccessor httpContextAccessor);
    Task SignOutAsync(IHttpContextAccessor httpContextAccessor);
  }
}
