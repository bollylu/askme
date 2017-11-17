using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskMeLib {
  public interface IRepository : IDisposable {

    bool IsInvalid { get; }

    bool Open();
    List<IQuestionFile> GetContent(string category = "", string language = "", bool recurse = false);
    string GetContentList(string category = "", string language = "", bool recurse = false);
    IQuestionFile GetFile(string filename);

  }
}
