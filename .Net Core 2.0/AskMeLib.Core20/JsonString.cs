using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AskMeLib {
  public class JsonString {
    public string Content { get; set; }
    public JsonString(string json) {
      Content = json;
    }

    public virtual dynamic Deserialize() {
      if (_IsValid()) {
        return JsonConvert.DeserializeObject(Content);
      }

      throw new ApplicationException("Unable to deserialize JsonString : invalid string");
    }

    private bool _IsValid() {
      try {
        JContainer.Parse(Content);
        return true;
      } catch {
        return false;
      }
    }
    
  }

  
}
