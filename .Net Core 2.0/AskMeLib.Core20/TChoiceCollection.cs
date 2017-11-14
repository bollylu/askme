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
using System.Runtime.Serialization;
using BLTools.Json;

namespace AskMeLib {

  public partial class TChoiceCollection : TObjectBase, IChoiceCollection {

    #region --- XML constants ----------------------------------------------------------------------------------
    public const string XML_THIS_ELEMENT = "Choices"; 
    #endregion --- XML constants -------------------------------------------------------------------------------

    #region --- Public properties ------------------------------------------------------------------------------
    public List<IChoice> Items { get; set; } = new List<IChoice>();
    #endregion --- Public properties ---------------------------------------------------------------------------

    #region --- Constructor(s) ---------------------------------------------------------------------------------
    public TChoiceCollection() : base() {
    }

    public TChoiceCollection(XElement element) : base(element) {
      if (element.Name != XName.Get(XML_THIS_ELEMENT)) {
        return;
      }
      if (element.Elements(TChoice.XML_THIS_ELEMENT).Count() > 0) {
        foreach (XElement ChoiceItem in element.Elements(TChoice.XML_THIS_ELEMENT)) {
          Items.Add(new TChoice(ChoiceItem));
        }
      }
    }
    #endregion --- Constructor(s) ------------------------------------------------------------------------------

    public override IJsonValue ToJson() {
      JsonArray RetVal = new JsonArray();
      foreach(IChoice ItemItem in Items) {
        RetVal.AddItem(ItemItem.ToJson());
      }
      return RetVal;
    }
  }
}
