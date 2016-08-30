using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace AskMeLib {

  public partial class TQuestionFile : TXmlBase, IDisposable, IQuestionFile {

    #region --- XML constants ----------------------------------------------------------------------------------
    public const string XML_THIS_ELEMENT = "root";
    #endregion --- XML constants -------------------------------------------------------------------------------

    public const string QUESTION_FILE_EXTENSION = ".qcm";

    #region --- Public properties ------------------------------------------------------------------------------
    
    public List<TQuestionCollection> Items { get; set; } = new List<TQuestionCollection>();

    public IQuestionFileHeader Header {
      get {
        if (_Header == null) {
          _Header = ReadHeader();
        }
        return _Header;
      }
      set {
        _Header = null;
      }
    }
    protected IQuestionFileHeader _Header;
    #endregion --- Public properties ---------------------------------------------------------------------------

    #region --- Constructor(s) --------------------------------------------------------------------
    public TQuestionFile(string location) : base() {
      StorageLocation = location;
    }

    public TQuestionFile(IQuestionFile questionFile) : base(questionFile) {
      if (questionFile == null) {
        return;
      }
      StorageLocation = questionFile.StorageLocation;
      foreach(IQuestionCollection QuestionCollectionItem in questionFile.Items) {
        Items.Add(new TQuestionCollection(QuestionCollectionItem));
      }
    }

    public void Dispose() {
      Items.Clear();
    }
    #endregion --- Constructor(s) -----------------------------------------------------------------

    #region --- Converters ------------------------------------------------------------------------
    public override string ToString() {
      StringBuilder RetVal = new StringBuilder();
      RetVal.Append($"File : {StorageLocation}");
      RetVal.Append($", {Header}");
      return RetVal.ToString();
    }
    #endregion --- Converters ---------------------------------------------------------------------

    public void ReadData(string location = "") {
      #region Validate parameters
      if (location != "") {
        StorageLocation = location;
      }
      if (!File.Exists(StorageLocation)) {
        return;
      }
      #endregion Validate parameters

      Items.Clear();
      Trace.WriteLine($"Loading {StorageLocation}");
      try {
        XDocument MyData = XDocument.Load(StorageLocation);
        XElement Root = MyData.Element(XML_THIS_ELEMENT);
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

    public IQuestionFileHeader ReadHeader(string location = "") {
      #region Validate parameters
      if (location != "") {
        StorageLocation = location;
      }
      if (!File.Exists(StorageLocation)) {
        return null;
      }
      #endregion Validate parameters

      Trace.WriteLine($"Reading header of {StorageLocation}");
      TQuestionFileHeader FileHeader;
      XDocument MyData;
      try {
        MyData = XDocument.Load(StorageLocation);
        XElement Root = MyData.Element(XML_THIS_ELEMENT);
        FileHeader = new TQuestionFileHeader(Root.Element(TQuestionFileHeader.XML_THIS_ELEMENT));
        return FileHeader;
      } catch (Exception ex) {
        Trace.WriteLine($"Reading error : {ex.Message}");
        return null;
      }
    }

    public string GetHeaderText() {
      return ReadHeader().ToString();
    }

    public string GetHeaderTextWithDetails() {
      ReadData();
      StringBuilder RetVal = new StringBuilder(Header.ToString());
      RetVal.Append($", {Items.Count()} collection(s) of questions");
      return RetVal.ToString();
    }

    public bool IsLanguageMatching(string language = "") {
      #region === Validate parameters ===
      if (Header == null) {
        return false;
      } 
      #endregion === Validate parameters ===
      return Header.Language.ToLower() == language.ToLower();
    }
  }
}
