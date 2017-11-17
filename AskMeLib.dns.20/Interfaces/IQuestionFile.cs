using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskMeLib {
  public interface IQuestionFile : IObjectBase {

    List<IQuestionCollection> Items { get; }
    IQuestionFileHeader Header { get; }

    List<IQuestionCollection> ReadData(string location = "");
    IQuestionFileHeader ReadHeader(string location = "");
    bool IsLanguageMatching(string language);
    string GetHeaderTextWithDetails();

  }
}
