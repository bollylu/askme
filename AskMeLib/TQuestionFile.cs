﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AskMeLib {
  public class TQuestionFile :TXmlBase {

    public const string XML_THIS_ELEMENT = "root";
    

    public const string QUESTION_FILE_EXTENSION = "*.qcm";

    public string Location { get; set; }
    public List<TQuestionCollection> Items { get; set; } = new List<TQuestionCollection>();

    public TQuestionFileHeader Header {
      get {
        if (_Header==null) {
          _Header = ReadHeader();
        }
        return _Header;
      }
      set {
        _Header = null;
      }
    }
    protected TQuestionFileHeader _Header;

    #region --- Constructor(s) --------------------------------------------------------------------
    public TQuestionFile(string location) : base() {
      Location = location;
    }
    #endregion --- Constructor(s) -----------------------------------------------------------------

    #region --- Converters ------------------------------------------------------------------------
    public override string ToString() {
      StringBuilder RetVal = new StringBuilder();
      RetVal.Append($"File : {Location}");
      RetVal.Append($", {Header}");
      return RetVal.ToString();
    } 
    #endregion --- Converters ---------------------------------------------------------------------

    public void Load(string location = "") {
      #region Validate parameters
      if (location != "") {
        Location = location;
      }
      if (!File.Exists(Location)) {
        return;
      }
      #endregion Validate parameters

      Items.Clear();
      Trace.WriteLine($"Loading {Location}");
      try {
        XDocument MyData = XDocument.Load(Location);
        XElement Root = MyData.Element(XML_THIS_ELEMENT);
        _Header = new TQuestionFileHeader(Root.Element(TQuestionFileHeader.XML_THIS_ELEMENT));
        IEnumerable<XElement> QuestionCollections = Root.Elements(TQuestionCollection.XML_THIS_ELEMENT);
        if (QuestionCollections.Count() > 0) {
          foreach (XElement QuestionCollectionItem in QuestionCollections) {
            Items.Add(new TQuestionCollection(QuestionCollectionItem));
          }
        }


      } catch (Exception ex) {
        Trace.WriteLine($"Reading error : {ex.Message}");
        return;
      }
    }

    public TQuestionFileHeader ReadHeader(string location="") {
      #region Validate parameters
      if (location != "") {
        Location = location;
      }
      if (!File.Exists(Location)) {
        return null;
      }
      #endregion Validate parameters

      Trace.WriteLine($"Reading header of {Location}");
      TQuestionFileHeader FileHeader;
      XDocument MyData;
      try {
        MyData = XDocument.Load(Location);
        XElement Root = MyData.Element(XML_THIS_ELEMENT);
        FileHeader = new TQuestionFileHeader(Root.Element(TQuestionFileHeader.XML_THIS_ELEMENT));
        return FileHeader;
      } catch (Exception ex) {
        Trace.WriteLine($"Reading error : {ex.Message}");
        return null;
      }
    }

    public string HeaderText() {
      return ReadHeader().ToString();
    }

    public string HeaderTextWithDetails() {
      Load();
      StringBuilder RetVal = new StringBuilder(Header.ToString());
      RetVal.Append($", {Items.Count()} collection(s) of questions");
      return RetVal.ToString();
    }

  }
}
