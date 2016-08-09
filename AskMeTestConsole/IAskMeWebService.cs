using AskMeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Runtime.Serialization;

namespace AskMeTestConsole {

  [ServiceContract]
  public interface IAskMeWebService {
    [OperationContract]
    string GetRepositoryList();
  
    [OperationContract]
    TQuestionFile GetQuestionFile(string filename);
  }

}
