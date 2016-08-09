using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLTools;
using System.Xml.Linq;
using BLTools.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using AskMeLib;

namespace AskMeWebService {

  [DataContract]
  public class TQuestionWCF : TXmlBaseWCF {

    #region --- Public properties ------------------------------------------------------------------------------
    [DataMember]
    public string QuestionType { get; set; }
    [DataMember]
    public TChoiceCollectionWCF Choices { get; set; } = new TChoiceCollectionWCF();
    #endregion --- Public properties ---------------------------------------------------------------------------

    public TQuestionWCF(IQuestion question) : base(question) {
      QuestionType = question.QuestionType;
      Choices = new TChoiceCollectionWCF(question.Choices);
    }
  }
}
