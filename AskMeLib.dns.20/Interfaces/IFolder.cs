using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskMeLib {
  public interface IFolder {

    string Location { get; set; }
    IFolderHeader Header { get; set; }
    List<IQuestionFile> Items { get; set; }
    List<IFolder> SubFolders { get; set; }

  }
}
