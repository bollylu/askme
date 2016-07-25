using BLTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AskMeLib {
  public class TXmlBase : IToXml {

    public const string XML_ATTRIBUTE_NAME = "Name";
    public const string XML_ATTRIBUTE_DESCRIPTION = "Description";

    public string Name { get; set; }
    public string Description { get; set; }


    public TXmlBase() {
      Name = "";
      Description = "";
    }

    public TXmlBase(XElement element) {
      Name = element.SafeReadAttribute<string>(XML_ATTRIBUTE_NAME, "");
      Description = element.SafeReadAttribute<string>(XML_ATTRIBUTE_DESCRIPTION, "");
    }

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

  }

}
