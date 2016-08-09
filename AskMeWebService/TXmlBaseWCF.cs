using BLTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.ServiceModel;
using System.Runtime.Serialization;
using AskMeLib;


namespace AskMeWebService {

  [DataContract]
  public class TXmlBaseWCF {

    #region --- Public properties ------------------------------------------------------------------------------
    [DataMember]
    public string Name { get; set; } = "";
    [DataMember]
    public string Description { get; set; } = "";
    #endregion --- Public properties ---------------------------------------------------------------------------

    public TXmlBaseWCF() { }

    public TXmlBaseWCF(IXmlBase xmlBase) {
      Name = xmlBase.Name;
      Description = xmlBase.Description;
    }
   
  }

}
