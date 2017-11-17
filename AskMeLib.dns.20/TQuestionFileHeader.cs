using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BLTools;
using System.Runtime.Serialization;
using BLTools.Json;

namespace AskMeLib {

  public partial class TQuestionFileHeader : TObjectBase, IQuestionFileHeader {

    #region --- XML constants ----------------------------------------------------------------------------------
    public const string XML_THIS_ELEMENT = "Header";
    public const string XML_ATTRIBUTE_CREATION_TIME = "CreationTime";
    public const string XML_ATTRIBUTE_CREATED_BY = "CreatedBy";
    public const string XML_ATTRIBUTE_LANGUAGE = "Language";
    public const string XML_ATTRIBUTE_CATEGORY = "Category";
    #endregion --- XML constants -------------------------------------------------------------------------------

    #region --- Public properties ------------------------------------------------------------------------------
    public DateTime CreationTime { get; set; } = DateTime.MinValue;
    public string CreatedBy { get; set; }
    public string Language { get; set; } = "FR";
    public string Category { get; set; } = "";
    DateTime IQuestionFileHeader.CreationTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    string IQuestionFileHeader.CreatedBy { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    string IQuestionFileHeader.Language { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    string IQuestionFileHeader.Category { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    #endregion --- Public properties ---------------------------------------------------------------------------

    #region --- Constructor(s) ---------------------------------------------------------------------------------
    public TQuestionFileHeader() : base() {
    }

    public TQuestionFileHeader(XElement element) : base(element) {
      CreationTime = element.SafeReadAttribute<DateTime>(XML_ATTRIBUTE_CREATION_TIME, DateTime.MinValue);
      CreatedBy = element.SafeReadAttribute<string>(XML_ATTRIBUTE_CREATED_BY, "");
      Language = element.SafeReadAttribute<string>(XML_ATTRIBUTE_LANGUAGE);
      Category = element.SafeReadAttribute<string>(XML_ATTRIBUTE_CATEGORY);
    }

    public TQuestionFileHeader(JsonObject jsonObject) : base(jsonObject) {
      CreationTime = jsonObject.SafeGetValueFirst<DateTime>(XML_ATTRIBUTE_CREATION_TIME, DateTime.MinValue);
      CreatedBy = jsonObject.SafeGetValueFirst<string>(XML_ATTRIBUTE_CREATED_BY, "");
      Language = jsonObject.SafeGetValueFirst<string>(XML_ATTRIBUTE_LANGUAGE);
      Category = jsonObject.SafeGetValueFirst<string>(XML_ATTRIBUTE_CATEGORY);
    }

    static public IQuestionFileHeader Create() {
      return new TQuestionFileHeader();
    }

    static public IQuestionFileHeader Create(XElement element) {
      return new TQuestionFileHeader(element);
    }

    static public IQuestionFileHeader Create(JsonObject jsonObject) {
      if (jsonObject==null) {
        return new TQuestionFileHeader();
      }
      return new TQuestionFileHeader(jsonObject);
    }

    public void Dispose() {
      Language = null;
      Category = null;
    }
    #endregion --- Constructor(s) ------------------------------------------------------------------------------

    #region --- Converters -------------------------------------------------------------------------------------
    public override string ToString() {
      StringBuilder RetVal = new StringBuilder();
      RetVal.Append($"Name : {Name}");
      RetVal.Append($", Category : {(Category == "" ? "Category unknown" : Category)}");
      RetVal.Append($", Description : {Description}");
      RetVal.Append($", Langage : {Language}");
      RetVal.Append($", Created by : {CreatedBy}");
      RetVal.Append($", Created on : {CreationTime.ToDMYHMS()}");
      return RetVal.ToString();
    }

    public override IJsonValue ToJson() {
      JsonObject RetVal = base.ToJson() as JsonObject;

      RetVal.Add(XML_ATTRIBUTE_CREATED_BY, CreatedBy);
      RetVal.Add(XML_ATTRIBUTE_CREATION_TIME, CreationTime);
      RetVal.Add(XML_ATTRIBUTE_LANGUAGE, Language);
      RetVal.Add(XML_ATTRIBUTE_CATEGORY, Category);

      return RetVal;
    }
    #endregion --- Converters -------------------------------------------------------------------------------------

    public bool IsLanguageMatching(string language = "") {
      return Language.ToLower() == language.ToLower();
    }
    
  }
}
