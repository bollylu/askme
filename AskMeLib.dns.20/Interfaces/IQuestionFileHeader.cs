using BLTools.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AskMeLib {
  public interface IQuestionFileHeader : IObjectBase, IDisposable {

    DateTime CreationTime { get; set; }
    string CreatedBy { get; set; }
    string Language { get; set; }
    string Category { get; set; }

    bool IsLanguageMatching(string language = "");

  }
}
