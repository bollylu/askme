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


namespace AskMeLib {

  [DataContract]
  public partial class TChoiceCollection : TXmlBase, IChoiceCollection {

    #region --- XML constants ----------------------------------------------------------------------------------
    public const string XML_THIS_ELEMENT = "Choices"; 
    #endregion --- XML constants -------------------------------------------------------------------------------

    #region --- Public properties ------------------------------------------------------------------------------
    [DataMember]
    public List<IChoice> Items { get; set; } = new List<IChoice>();
    #endregion --- Public properties ---------------------------------------------------------------------------

    #region --- Constructor(s) ---------------------------------------------------------------------------------
    public TChoiceCollection() : base() {
    }

    public TChoiceCollection(XElement element) : base(element) {
      if (element.Name != XName.Get(XML_THIS_ELEMENT)) {
        return;
      }
      foreach (XElement ChoiceItem in element.Elements(TChoice.XML_THIS_ELEMENT)) {
        Items.Add(new TChoice(ChoiceItem));
      }
    } 
    #endregion --- Constructor(s) ------------------------------------------------------------------------------

  }
}
