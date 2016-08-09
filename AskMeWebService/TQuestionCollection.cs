using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLTools;
using BLTools.ConsoleExtension;
using System.Xml.Linq;
using System.IO;
using System.Diagnostics;
using System.ServiceModel;
using System.Runtime.Serialization;
using AskMeLib;

namespace AskMeWebService {

  [DataContract]
  public class TQuestionCollectionWCF : TXmlBaseWCF {

    #region --- Properties ------------------------------------------------------------------------
    [DataMember]
    public List<TQuestionWCF> Items { get; set; } = new List<TQuestionWCF>();
    #endregion --- Properties ---------------------------------------------------------------------


    public TQuestionCollectionWCF(IQuestionCollection questionCollection) : base(questionCollection) {
      foreach (IQuestion QuestionItem in questionCollection.Items) {
        Items.Add(new TQuestionWCF(QuestionItem));
      }
    }
  }
}
