using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace AskMeLib {
  public class JsonString {
    public string Content { get; set; }
    public JsonString(string json) {
      Content = json;
    }

    public static JsonString Empty => new JsonString("");

    private bool _IsValid() {
      try {
        JContainer.Parse(Content);
        return true;
      } catch {
        return false;
      }
    }

    public T SafeGetValue<T>(string name, T defaultValue) {
      if ( string.IsNullOrWhiteSpace(name) ) {
        Trace.WriteLine("Unable to get value from JsonString : name is invalid");
        throw new ArgumentException($"Unable to get value from JsonString : {nameof(name)} is invalid", nameof(name));
      }

      try {
        T Temp = JToken.Parse(Content).Value<T>(name.ToLowerInvariant());
        if (Temp==null) {
          return defaultValue;
        }
        return Temp;
      } catch ( Exception ex ) {
        Trace.WriteLine($"Error parsing JsonString content : {ex.Message}");
        return defaultValue;
      }
    }

  }

}
