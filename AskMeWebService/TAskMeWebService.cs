using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AskMeLib;
using System.ServiceModel;

namespace AskMeWebService {
  public class TAskMeWebService : IAskMeWebService {

    public string GetRepositoryList() {
      using (TRepository Repository = new TRepository(@"i:\testrepo")) {
        return Repository.GetContentList();
      }
    }
    public IQuestionFile GetQuestionFile(string filename) {
      using (TRepository Repository = new TRepository(@"i:\testrepo")) {
        return Repository.GetFile(filename);
      }
    }
  }

}
