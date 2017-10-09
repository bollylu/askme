using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskMeLib {
  public interface IQuestionFileHeader : IXmlBase {

    DateTime CreationTime { get; set; }
    string CreatedBy { get; set; }
    string Language { get; set; }
    string Category { get; set; }

    bool IsLanguageMatching(string language = "");

  }
}
