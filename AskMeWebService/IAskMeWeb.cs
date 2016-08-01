using AskMeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Runtime.Serialization;

namespace AskMeWebService {

  [ServiceContract]
  public interface IAskMeWebGetContent {
    [OperationContract]
    string GetContentList();
  }

  [ServiceContract]
  public interface IAskMeWebGetFile {
    [OperationContract]
    string GetFile(string filename);
  }

  public class TResponseFile {
    public TQuestionFile Content;
  }
}
