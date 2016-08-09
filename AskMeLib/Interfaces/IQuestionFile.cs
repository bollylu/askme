using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskMeLib {
  public interface IQuestionFile : IXmlBase {

    string Location { get; set; }
    List<TQuestionCollection> Items { get; set; }
    IQuestionFileHeader Header { get; set; }

    void ReadData(string location = "");
    IQuestionFileHeader ReadHeader(string location = "");
    bool IsLanguageMatching(string language);
    string GetHeaderTextWithDetails();

  }
}
