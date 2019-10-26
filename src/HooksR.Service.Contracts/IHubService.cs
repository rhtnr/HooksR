using HooksR.DTO;
using HooksR.Entities;
using System.Threading.Tasks;

namespace HooksR.Service.Contracts
{
  public interface IHubService
  {
    Task SendAsSync(string recieve, UIPushEvent hooksREvent);
  }
}
