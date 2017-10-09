using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskMeLib {
  public interface IQuestion : IXmlBase {

    string QuestionType { get; set; }
    IChoiceCollection Choices { get; set; }
    int CurrentChoice { get; set; }

    string ToJSon();
    bool Ask();

  }
}
