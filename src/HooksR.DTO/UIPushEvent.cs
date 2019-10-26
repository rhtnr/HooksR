using System;

namespace HooksR.DTO
{
  public class UIPushEvent : UIPush
  {
    public UIPushRequest Request { get; set; }
    public UIPushResponse Response { get; set; }
  }
}
