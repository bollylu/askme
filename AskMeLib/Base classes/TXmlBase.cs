using BLTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AskMeLib {
  public class TXmlBase : IToXml {

    #region --- XML constants ----------------------------------------------------------------------------------
    public const string XML_ATTRIBUTE_NAME = "Name";
    public const string XML_ATTRIBUTE_DESCRIPTION = "Description";
    #endregion --- XML constants -------------------------------------------------------------------------------

    #region --- Public properties ------------------------------------------------------------------------------
    public string Name { get; set; }
    public string Description { get; set; }
    #endregion --- Public properties ---------------------------------------------------------------------------


    #region --- Constructor(s) ---------------------------------------------------------------------------------
    public TXmlBase() {
      Name = "";
      Description = "";
    }

    public TXmlBase(XElement element) {
      Name = element.SafeReadAttribute<string>(XML_ATTRIBUTE_NAME, "");
      Description = element.SafeReadAttribute<string>(XML_ATTRIBUTE_DESCRIPTION, "");
    }
    #endregion --- Constructor(s) ------------------------------------------------------------------------------

    #region --- Converters -------------------------------------------------------------------------------------
    public virtual XElement ToXml() {
      return null;
    }

    public virtual XElement ToXml(string name) {
      XElement RetVal = new XElement(name);
      return RetVal;
    }

    public virtual XElement ToXml(XName name) {
      XElement RetVal = new XElement(name);
      return RetVal;
    } 
    #endregion --- Converters -------------------------------------------------------------------------------------

  }

}
