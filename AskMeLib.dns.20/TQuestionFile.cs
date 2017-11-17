using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Runtime.Serialization;
using BLTools;
using BLTools.Json;

namespace AskMeLib {

  public partial class TQuestionFile : TObjectBase, IDisposable, IQuestionFile {

    #region --- XML constants ----------------------------------------------------------------------------------
    public const string XML_THIS_ELEMENT = "root";
    public static readonly string XML_ELEMENT_QUESTIONS = "ListOfQuestions";
    #endregion --- XML constants -------------------------------------------------------------------------------

    public const string QUESTION_FILE_EXTENSION = ".qcm";

    #region --- Public properties ------------------------------------------------------------------------------

    public List<IQuestionCollection> Items {
      get {
        lock ( _LockItems ) {
          if ( _Items == null ) {
            _Items = ReadData();
          }
          return _Items;
        }
      }
    }
    private List<IQuestionCollection> _Items;

    public IQuestionFileHeader Header {
      get {
        lock ( _LockHeader ) {
          if ( _Header == null ) {
            _Header = ReadHeader();
          }
          return _Header;
        }
      }
      protected set {
        lock ( _LockHeader ) {
          _Header = value;
        }
      }
    }
    protected IQuestionFileHeader _Header;

    public override string Name {
      get {
        if ( _Header != null ) {
          return _Header.Name;
        }
        return "";
      }
      set {
        if ( _Header != null ) {
          _Header.Name = value;
        }
      }
    }

    #endregion --- Public properties ---------------------------------------------------------------------------

    private object _LockHeader = new object();
    private object _LockItems = new object();

    #region --- Constructor(s) --------------------------------------------------------------------
    public TQuestionFile(string location) : base() {
      StorageLocation = location;
    }

    public TQuestionFile(IQuestionFile questionFile) : base(questionFile) {
      if ( questionFile == null ) {
        return;
      }
      StorageLocation = questionFile.StorageLocation;
      foreach ( IQuestionCollection QuestionCollectionItem in questionFile.Items ) {
        Items.Add(new TQuestionCollection(QuestionCollectionItem));
      }
    }

    public TQuestionFile(JsonObject jsonObject) : base(jsonObject) {
      JsonObject JsonHeader = jsonObject.SafeGetValueFirst<JsonObject>(TQuestionFileHeader.XML_THIS_ELEMENT);
      Header = TQuestionFileHeader.Create(JsonHeader);
      lock ( _LockItems ) {
        _Items = new List<IQuestionCollection>();
        JsonArray ListOfQuestionCollection = jsonObject.SafeGetValueFirst<JsonArray>(XML_ELEMENT_QUESTIONS);
        foreach ( JsonObject QuestionCollectionItem in ListOfQuestionCollection ) {
          _Items.Add(new TQuestionCollection(QuestionCollectionItem));
        }
      }
    }

    public void Dispose() {
      lock ( _LockItems ) {
        Items.Clear();
      }
      lock ( _LockHeader ) {
        Header.Dispose();
      }
    }
    #endregion --- Constructor(s) -----------------------------------------------------------------

    #region --- Converters ------------------------------------------------------------------------
    public override string ToString() {
      StringBuilder RetVal = new StringBuilder();
      RetVal.Append($"File : {StorageLocation}");
      lock ( _LockHeader ) {
        RetVal.Append($", {Header}");
      }
      return RetVal.ToString();
    }

    public override IJsonValue ToJson() {
      JsonObject RetVal = base.ToJson() as JsonObject;
      RetVal.Add(TQuestionFileHeader.XML_THIS_ELEMENT, Header.ToJson());
      JsonArray Questions = new JsonArray();
      lock ( _LockItems ) {
        foreach ( IQuestionCollection QuestionCollectionItem in Items ) {
          Questions.Add(QuestionCollectionItem.ToJson());
        }
      }
      RetVal.Add(XML_ELEMENT_QUESTIONS, Questions);
      return RetVal;
    }
    #endregion --- Converters ---------------------------------------------------------------------

    public List<IQuestionCollection> ReadData(string location = "") {
      lock ( _LockItems ) {
        if ( _Items == null ) {
          _Items = new List<IQuestionCollection>();
        }
        _Items.Clear();

        #region Validate parameters
        if ( location != "" ) {
          StorageLocation = location;
        }
        if ( !File.Exists(StorageLocation) ) {
          return _Items;
        }
        #endregion Validate parameters

        Trace.WriteLine($"Loading {StorageLocation}");
        try {
          XDocument MyData = XDocument.Load(StorageLocation);
          XElement Root = MyData.Element(XML_THIS_ELEMENT);
          IEnumerable<XElement> QuestionCollections = Root.Elements(TQuestionCollection.XML_THIS_ELEMENT);
          if ( QuestionCollections.Count() > 0 ) {
            foreach ( XElement QuestionCollectionItem in QuestionCollections ) {
              _Items.Add(new TQuestionCollection(QuestionCollectionItem));
            }
          }


        } catch ( Exception ex ) {
          Trace.WriteLine($"Reading error : {ex.Message}");
          return _Items;
        }

        return _Items;
      }
    }

    public IQuestionFileHeader ReadHeader(string location = "") {
      #region Validate parameters
      if ( location != "" ) {
        StorageLocation = location;
      }
      if ( !File.Exists(StorageLocation) ) {
        return null;
      }
      #endregion Validate parameters

      Trace.WriteLine($"Reading header of {StorageLocation}");
      XDocument MyData;
      try {
        MyData = XDocument.Load(StorageLocation);
        XElement Root = MyData.Element(XML_THIS_ELEMENT);
        lock ( _LockHeader ) {
          _Header = new TQuestionFileHeader(Root.Element(TQuestionFileHeader.XML_THIS_ELEMENT));
        }
        return _Header;
      } catch ( Exception ex ) {
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
      if ( Header == null ) {
        return false;
      }
      #endregion === Validate parameters ===
      return Header.Language.ToLower() == language.ToLower();
    }


  }
}
