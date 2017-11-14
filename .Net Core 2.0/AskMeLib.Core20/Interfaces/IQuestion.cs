using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskMeLib {
  public interface IQuestion : IObjectBase {

    string QuestionType { get; set; }
    IChoiceCollection Choices { get; set; }
    int CurrentChoice { get; set; }

    bool Render();

  }
}
