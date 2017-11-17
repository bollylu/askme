using BLTools.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskMeLib {
  public interface IObjectBase {
    string Id { get; }
    string Name { get; set; }
    string Description { get; set; }

    string StorageLocation { get; set; }

    IJsonValue ToJson();
  }
}
