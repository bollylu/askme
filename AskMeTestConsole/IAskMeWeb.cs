using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AskMeTestConsole {
  [ServiceContract]
  public interface IAskMeWeb {
    [OperationContract]
    string GetContentList();
  }
}
