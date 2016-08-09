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
  public partial class TChoiceCollectionWCF : TXmlBaseWCF {

    #region --- Public properties ------------------------------------------------------------------------------
    [DataMember]
    public List<TChoiceWCF> Items { get; set; } = new List<TChoiceWCF>();
    #endregion --- Public properties ---------------------------------------------------------------------------

    public TChoiceCollectionWCF() : base() { }

    public TChoiceCollectionWCF(IChoiceCollection choiceCollection) : base(choiceCollection) {
      foreach(IChoice ChoiceItem in choiceCollection.Items) {
        Items.Add(new TChoiceWCF(ChoiceItem));
      }
    }
  }
}
