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
    public const string XML_ATTRIBUTE_CATEGORY = "Category";

    public DateTime CreationTime { get; set; } = DateTime.MinValue;
    public string CreatedBy { get; set; }
    public string Language { get; set; } = "FR";
    public string Category { get; set; } = "";
    public List<TQuestionCollection> Items { get; set; } = new List<TQuestionCollection>();

    public TQuestionFileHeader() : base() {
    }

    public TQuestionFileHeader(XElement element) : base(element) {
      CreationTime = element.SafeReadAttribute<DateTime>(XML_ATTRIBUTE_CREATION_TIME, DateTime.MinValue);
      CreatedBy = element.SafeReadAttribute<string>(XML_ATTRIBUTE_CREATED_BY, "");
      Language = element.SafeReadAttribute<string>(XML_ATTRIBUTE_LANGUAGE);
      Category = element.SafeReadAttribute<string>(XML_ATTRIBUTE_CATEGORY);
    }


    public override string ToString() {
      StringBuilder RetVal = new StringBuilder();
      RetVal.Append($"Name : {Name}");
      RetVal.Append($", Category : {(Category=="" ? "Category unknown" : Category)}");
      RetVal.Append($", Description : {Description}");
      RetVal.Append($", Langage : {Language}");
      RetVal.Append($", Created by : {CreatedBy}");
      RetVal.Append($", Created on : {CreationTime.ToDMYHMS()}");
      return RetVal.ToString();
    }
  }
}
