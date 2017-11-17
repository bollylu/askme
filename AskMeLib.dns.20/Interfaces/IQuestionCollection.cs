using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskMeLib {
  public interface IQuestionCollection : IObjectBase {

    List<IQuestion> Items { get; set; }
    int Counter { get; set; }

    void Render();

  }
}
