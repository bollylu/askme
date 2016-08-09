using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.ServiceModel;
using System.Runtime.Serialization;
using AskMeLib;

namespace AskMeWebService {

  [DataContract]
  public class TQuestionFileWCF : TXmlBaseWCF, IQuestionFile {

    #region --- Public properties ------------------------------------------------------------------------------
    [DataMember]
    public List<TQuestionCollectionWCF> Items { get; set; } = new List<TQuestionCollectionWCF>();

    [DataMember]
    public TQuestionFileHeaderWCF Header { get; set; }

    List<TQuestionCollection> IQuestionFile.Items {
      get {
        throw new NotImplementedException();
      }

      set {
        throw new NotImplementedException();
      }
    }

    IQuestionFileHeader IQuestionFile.Header {
      get {
        throw new NotImplementedException();
      }

      set {
        throw new NotImplementedException();
      }
    }

    public string Location {
      get {
        throw new NotImplementedException();
      }

      set {
        throw new NotImplementedException();
      }
    }
    #endregion --- Public properties ---------------------------------------------------------------------------

    public TQuestionFileWCF(IQuestionFile questionFile) : base(questionFile) {
      if (questionFile == null) {
        return;
      }
      foreach (TQuestionCollection QuestionCollectionItem in questionFile.Items) {
        Items.Add(new TQuestionCollectionWCF(QuestionCollectionItem));

      }
    }
    #region --- Converters ------------------------------------------------------------------------
    public override string ToString() {
      StringBuilder RetVal = new StringBuilder();
      RetVal.Append($"File : {Location}");
      RetVal.Append($", {Header}");
      return RetVal.ToString();
    }
    #endregion --- Converters ---------------------------------------------------------------------

    public void ReadData(string location = "") {
      throw new NotImplementedException();
    }

    public IQuestionFileHeader ReadHeader(string location = "") {
      throw new NotImplementedException();
    }

    public bool IsLanguageMatching(string language) {
      throw new NotImplementedException();
    }

    public string GetHeaderTextWithDetails() {
      throw new NotImplementedException();
    }
  }
}
