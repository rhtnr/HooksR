using System;
using System.Collections.Generic;

namespace HooksR.Entities
{
  public class WebRequest
  {
    public List<KeyValuePair<String, IEnumerable<String>>> Headers { get; set; }
    public String Body { get; set; }
    public String QueryString { get; set; }

    public String Scheme { get; set;}

    public String Host { get; set; }
     
  }
}
