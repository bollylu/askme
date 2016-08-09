using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AskMeLib;
using System.ServiceModel;
using System.Diagnostics;

namespace AskMeWebService {
  public class TAskMeWebService : IAskMeWebService {

    public string GetRepositoryList() {
      using (TRepository Repository = new TRepository(@"i:\testrepo")) {
        return Repository.GetContentList();
      }
    }
    public TQuestionFileWCF GetQuestionFile(string filename) {
      Trace.WriteLine($"Requesting {filename}");

      using (TRepository Repository = new TRepository(@"i:\testrepo")) {
        IQuestionFile TempFile = Repository.GetFile(filename);
        if (TempFile == null) {
          Trace.WriteLine($"File not found or access denied : {TempFile.ToString()}");
          return null;
        }
        TQuestionFileWCF RetVal = new TQuestionFileWCF(TempFile);
        Trace.WriteLine($"Returning {RetVal.ToString()}");
        return RetVal;
      }
    }
  }

}
