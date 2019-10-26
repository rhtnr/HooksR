using System;
using System.Collections.Generic;
using System.Text;

namespace HooksR.DTO
{
  public class UIPushRequest: UIPush
  {
    public dynamic Headers { get; set; }
    public String Body { get; set; }
    public String QueryString { get; set; }

    public String Scheme { get; set; }

    public String Host { get; set; }
  }
}
