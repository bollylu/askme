using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLTools;
using System.Xml.Linq;
using System.ServiceModel;
using System.Runtime.Serialization;
using AskMeLib;


namespace AskMeWebService {

  [DataContract]
  public class TChoiceWCF : TXmlBaseWCF, IChoice {

    #region --- Public properties ------------------------------------------------------------------------------
    [DataMember]
    public bool IsCorrect { get; set; } = false;
    #endregion --- Public properties ---------------------------------------------------------------------------


    public TChoiceWCF(IChoice choice) : base(choice) {
      IsCorrect = choice.IsCorrect;
    }

  }
}
