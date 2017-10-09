using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskMeLib {
  public interface IChoice : IXmlBase {

    bool IsCorrect { get; set; }

  }
}
