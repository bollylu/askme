using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using BLTools;
using System.Collections.Generic;

namespace AskMeLib {
  public class JsonData : IEquatable<JsonData>, IComparable<JsonData> {

    public static JsonData Empty => new JsonData();
    public static JsonData EmptyObject => new JsonData(new object());

    #region --- Public properties ------------------------------------------------------------------------------
    private object _ContentLock = new object();

    public string Content { get; protected set; } = "";

    public Dictionary<string, object> RawContent { get; protected set; } = new Dictionary<string, object>();
    #endregion --- Public properties ---------------------------------------------------------------------------

    #region --- Constructor(s) ---------------------------------------------------------------------------------
    public JsonData() {
    }
    public JsonData(object jsonContent) {
      Initialize(jsonContent);
    }

    protected void Initialize(object jsonObject) {
      if ( jsonObject == null ) {
        return;
      }

      if ( jsonObject is JsonData ) {
        JsonData JsonDataSource = jsonObject as JsonData;
        lock ( _ContentLock ) {
          Content = JsonDataSource.Content;
          RawContent.Clear();
          foreach(KeyValuePair<string, object> JsonItem in JsonDataSource.RawContent) {
            RawContent.Add(JsonItem.Key, JsonItem.Value);
          }
        }
        return;
      }

      lock ( _ContentLock ) {
        Content = JsonConvert.SerializeObject(jsonObject);
        JToken ParsedValues = JToken.Parse(Content);
        if ( !ParsedValues.HasValues ) {
          return;
        }
        
      }
    }
    #endregion --- Constructor(s) ------------------------------------------------------------------------------

    public void Clear() {
      lock(_ContentLock) {
        Content = "";
        RawContent.Clear();
      }
      
    }

    public bool IsValid() {
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
        JToken ParsedValues = JToken.Parse(Content);
        if ( !ParsedValues.HasValues ) {
          return defaultValue;
        }
        JToken SelectedToken = ParsedValues.SelectToken(name, true);
        T Temp = SelectedToken.ToObject<T>();
        if ( Temp == null ) {
          return defaultValue;
        }
        return Temp;
      } catch ( Exception ex ) {
        Trace.WriteLine($"Error parsing JsonString content : {ex.Message}");
        return defaultValue;
      }
    }

    public void Add(JsonData newItem) {
      if ( newItem == null ) {
        return;
      }
      if ( this == Empty ) {
        Content = newItem.Content;
        return;
      }

      Content = $"{{ {Content.Substring(1).Left(Content.Length - 1)}, {newItem.Content} }}";
    }

    #region --- IEquatable --------------------------------------------
    public override bool Equals(object obj) {
      if ( obj == null ) {
        return false;
      }

      return Content.Equals(( (JsonData)obj ).Content);
    }

    public bool Equals(JsonData other) {
      if ( other == null ) {
        return false;
      }
      return Content.Equals(other.Content);
    }

    public override int GetHashCode() {
      return Content.GetHashCode();
    }
    #endregion --- IEquatable --------------------------------------------

    #region --- IComparable --------------------------------------------
    public int CompareTo(JsonData other) {
      return Content.CompareTo(other.Content);
    }
    #endregion --- IComparable --------------------------------------------
  }

}
