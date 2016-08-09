using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BLTools;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace AskMeWebService {

  [DataContract]
  public class TQuestionFileHeaderWCF : TXmlBaseWCF {

    #region --- Public properties ------------------------------------------------------------------------------
    [DataMember]
    public DateTime CreationTime { get; set; } = DateTime.MinValue;
    [DataMember]
    public string CreatedBy { get; set; }
    [DataMember]
    public string Language { get; set; } = "FR";
    [DataMember]
    public string Category { get; set; } = "";
    #endregion --- Public properties ---------------------------------------------------------------------------


  }
}
