using System;
using System.Collections.Generic;
using System.Net;

namespace HooksR.Entities
{
  public class WebRequest
  {
    public List<KeyValuePair<String, IEnumerable<String>>> Headers { get; set; }
    public String Body { get; set; }
    public String QueryString { get; set; }

    public String Scheme { get; set;}

    public String Method { get; set; }

    public String Path { get; set; }

    public String Host { get; set; }

    public IPAddress Source { get; set; }

    public DateTime TimeStamp { get; set; }
     
  }
}
