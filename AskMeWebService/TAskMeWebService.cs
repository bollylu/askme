using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AskMeLib;
using System.ServiceModel;

namespace AskMeWebService {
  public class TAskMeWebService : IAskMeWebGetContent, IAskMeWebGetFile {

    public string GetContentList() {
      using (TRepository Repository = new TRepository(@"i:\testrepo")) {
        return Repository.GetContentList();
      }
    }
    public string GetFile(string filename) {
      using (TRepository Repository = new TRepository(@"i:\testrepo")) {
        return ""; // Repository.GetFile(filename);
      }
    }
  }
}
