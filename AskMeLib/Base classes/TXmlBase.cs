using BLTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.ServiceModel;
using System.Runtime.Serialization;


namespace AskMeLib {

  public partial class TXmlBase : TNotificationBase, IToXml {

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

    public TXmlBase(IXmlBase xmlBase) {
      Name = xmlBase.Name;
      Description = xmlBase.Description;
    }
    #endregion --- Constructor(s) ------------------------------------------------------------------------------

    #region --- Converters -------------------------------------------------------------------------------------
    public virtual XElement ToXml() {
      throw new NotImplementedException();
    }

    public virtual XElement ToXml(string name) {
      XElement RetVal = new XElement(name);
      RetVal.SetAttributeValue(XML_ATTRIBUTE_NAME, Name);
      RetVal.SetAttributeValue(XML_ATTRIBUTE_DESCRIPTION, Description);
      return RetVal;
    }

    public virtual XElement ToXml(XName name) {
      XElement RetVal = new XElement(name);
      RetVal.SetAttributeValue(XML_ATTRIBUTE_NAME, Name);
      RetVal.SetAttributeValue(XML_ATTRIBUTE_DESCRIPTION, Description);
      return RetVal;
    } 
    #endregion --- Converters -------------------------------------------------------------------------------------

  }

}
