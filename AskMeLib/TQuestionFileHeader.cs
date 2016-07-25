using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BLTools;

namespace AskMeLib {
  public class TQuestionFileHeader :TXmlBase {

    public const string XML_THIS_ELEMENT = "Header";
    public const string XML_ATTRIBUTE_CREATION_TIME = "CreationTime";
    public const string XML_ATTRIBUTE_CREATED_BY = "CreatedBy";
    public const string XML_ATTRIBUTE_LANGUAGE = "Language";

    public DateTime CreationTime { get; set; } = DateTime.MinValue;
    public string CreatedBy { get; set; }
    public string Language { get; set; } = "FR";
    public List<TQuestionCollection> Items { get; set; } = new List<TQuestionCollection>();

    public TQuestionFileHeader() : base() {
    }

    public TQuestionFileHeader(XElement element) : base(element) {
      CreationTime = element.SafeReadAttribute<DateTime>(XML_ATTRIBUTE_CREATION_TIME, DateTime.MinValue);
      CreatedBy = element.SafeReadAttribute<string>(XML_ATTRIBUTE_CREATED_BY, "");
      Language = element.SafeReadAttribute<string>(XML_ATTRIBUTE_LANGUAGE);
    }
  }
}
