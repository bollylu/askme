﻿using BLTools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AskMeLib {
  public partial class TRepository : TXmlBase, IRepository {

    public const string DEFAULT_REPOSITORY_ROOT = @"i:\AskMeRepositories";
    public const string DEFAULT_REPOSITORY_PATH = "_Data";
    public const string DEFAULT_REPOSITORY_FILENAME = "repository.xml";
    public const string DEFAULT_DATA_FOLDER_NAME = "Data";
    public const string DEFAULT_DESC_FOLDER_NAME = "Desc";

    public const string XML_THIS_ELEMENT = "Repository";
    public const string XML_ATTRIBUTE_QCM_FOLDER = "QcmFolder";
    public const string XML_ATTRIBUTE_DESC_FOLDER = "DescFolder";

    #region --- Public properties -----------------------------------------------------------------
    [JsonIgnore]
    public static string GlobalRepositoryRoot {
      get {
        if (string.IsNullOrWhiteSpace(_GlobalRepositoryRoot)) {
          return DEFAULT_REPOSITORY_ROOT;
        }
        return _GlobalRepositoryRoot;
      }
      set {
        _GlobalRepositoryRoot = value;
      }
    }
    private static string _GlobalRepositoryRoot;

    [JsonIgnore]
    public string RepositoryRoot {
      get {
        if ( string.IsNullOrWhiteSpace(_RepositoryRoot) ) {
          return GlobalRepositoryRoot;
        }
        return _RepositoryRoot;
      }
      set {
        _RepositoryRoot = value;
      }
    }
    private string _RepositoryRoot;

    [JsonIgnore]
    public string RepositoryPath {
      get {
        if ( string.IsNullOrWhiteSpace(_RepositoryPath) ) {
          return Path.Combine(RepositoryRoot, DEFAULT_REPOSITORY_PATH);
        }
        return Path.Combine(RepositoryRoot, _RepositoryPath);
      }
      set {
        _RepositoryPath = value;
      }
    }
    private string _RepositoryPath;

    [JsonIgnore]
    public string RepositoryFilename => Path.Combine(RepositoryPath, DEFAULT_REPOSITORY_FILENAME);

    #region --- Data folder --------------------------------------------

    public string DataFolderName {
      get {
        if ( string.IsNullOrWhiteSpace(_DataFolderName) ) {
          return DEFAULT_DATA_FOLDER_NAME;
        }
        return _DataFolderName;
      }
      set {
        _DataFolderName = value;
      }
    }
    private string _DataFolderName;

    [JsonIgnore]
    public string CompleteDataFolderName => Path.Combine(RepositoryPath, DataFolderName);
    #endregion --- Data folder --------------------------------------------

    #region --- Desc folder --------------------------------------------
    public string DescFolderName {
      get {
        if ( string.IsNullOrWhiteSpace(_DescFolderName) ) {
          return DEFAULT_DESC_FOLDER_NAME;
        }
        return _DescFolderName;
      }
      set {
        _DescFolderName = value;
      }
    }
    private string _DescFolderName;

    [JsonIgnore]
    public string CompleteDescFolderName => Path.Combine(RepositoryPath, DescFolderName);
    #endregion --- Desc folder --------------------------------------------

    [JsonIgnore]
    public List<IQuestionFile> QFiles { get; set; } = new List<IQuestionFile>();

    [JsonIgnore]
    public bool IsInvalid {
      get {
        if ( string.IsNullOrWhiteSpace(StorageLocation) ) {
          return true;
        }
        if ( !File.Exists(StorageLocation) ) {
          return true;
        }
        return false;
      }
    }

    [JsonIgnore]
    public static object Empty => new object();
    #endregion --- Public properties --------------------------------------------------------------

    #region --- Constructor(s) --------------------------------------------------------------------
    public TRepository() : base() {
      StorageLocation = RepositoryFilename;
    }

    public TRepository(string folder) : base() {
      RepositoryPath = folder;
      StorageLocation = RepositoryFilename;
    }

    public TRepository(JsonString repository) {
      try {
        Name = repository.SafeGetValue<string>(nameof(Name), "");
        Description = repository.SafeGetValue<string>(nameof(Description), "");
        DataFolderName = repository.SafeGetValue<string>("toto", DEFAULT_DATA_FOLDER_NAME);
        DescFolderName = repository.SafeGetValue<string>(nameof(DescFolderName), DEFAULT_DESC_FOLDER_NAME);
        RepositoryPath = repository.SafeGetValue<string>(nameof(RepositoryPath), DEFAULT_REPOSITORY_PATH);
      } catch ( Exception ex ) {
        Trace.WriteLine($"Unable to create TRepository : {ex.Message}");
        throw new ArgumentException("Unable to create TRepository : JsonString error", nameof(repository));
      }
    }

    public TRepository(TRepository repository) {
      if ( repository == null ) {
        throw new ArgumentNullException(nameof(repository));
      }
      Name = repository.Name;
      Description = repository.Description;
      DataFolderName = repository.DataFolderName;
      DescFolderName = repository.DescFolderName;
      RepositoryPath = repository.RepositoryPath;
      StorageLocation = repository.StorageLocation;
    }

    public void Dispose() {
      QFiles.Clear();
    }
    #endregion --- Constructor(s) -----------------------------------------------------------------

    #region --- Converters ------------------------------------------------------------------------
    public override string ToString() {
      StringBuilder RetVal = new StringBuilder();
      RetVal.Append($"Name = {Name}");
      RetVal.Append($", Description = {Description}");
      RetVal.Append($", Data = {DataFolderName}");
      RetVal.Append($", Rich = {DescFolderName}");
      RetVal.Append($", Content = {QFiles.Count()} files");
      return RetVal.ToString();
    }

    public virtual JsonString ToJson(int subLevels = 0) {
      return new JsonString(JsonConvert.SerializeObject(this));
    }

    public override XElement ToXml() {
      XElement RetVal = base.ToXml(XML_THIS_ELEMENT);
      RetVal.Add(XML_ATTRIBUTE_QCM_FOLDER, DataFolderName);
      RetVal.Add(XML_ATTRIBUTE_DESC_FOLDER, DescFolderName);
      return RetVal;
    }
    #endregion --- Converters ---------------------------------------------------------------------

    public bool Open() {
      if ( IsInvalid ) {
        return false;
      }

      try {
        XElement Header = LoadXml(RepositoryFilename).SafeReadElement(TRepository.XML_THIS_ELEMENT);
        if ( Header == null ) {
          return false;
        }

        Name = Header.SafeReadAttribute<string>(XML_ATTRIBUTE_NAME, "");
        Description = Header.SafeReadAttribute<string>(XML_ATTRIBUTE_DESCRIPTION, "");
        DataFolderName = Header.SafeReadAttribute<string>(XML_ATTRIBUTE_QCM_FOLDER, DEFAULT_DATA_FOLDER_NAME);
        DescFolderName = Header.SafeReadAttribute<String>(XML_ATTRIBUTE_DESC_FOLDER, DEFAULT_DESC_FOLDER_NAME);

        return true;
      } catch {
        return false;
      }
    }

    public virtual List<IQuestionFile> GetContent(string category = "", string language = "", bool recurse = false) {
      QFiles.Clear();

      if ( string.IsNullOrWhiteSpace(DataFolderName) ) {
        return QFiles;
      }

      string CurrentDataFolder = Path.Combine(StorageLocation, DataFolderName);

      if ( !Directory.Exists(CurrentDataFolder) ) {
        return QFiles;
      }

      foreach ( string FileItem in Directory.GetFiles(CurrentDataFolder, $"*{TQuestionFile.QUESTION_FILE_EXTENSION}", SearchOption.AllDirectories) ) {
        TQuestionFile TempFile = new TQuestionFile(FileItem);
        if ( ( category == "" || TempFile.Header.Category.ToLower().Contains(category.ToLower()) )
          && ( language == "" || TempFile.Header.IsLanguageMatching(language) ) ) {
          QFiles.Add(TempFile);
        }
      }
      return QFiles;

    }

    public virtual string GetContentList(string category = "", string language = "", bool recurse = false) {
      StringBuilder RetVal = new StringBuilder();
      IEnumerable<IQuestionFile> Content = GetContent(category, language, recurse);
      if ( Content == null || Content.Count() == 0 ) {
        return "";
      }
      foreach ( IQuestionFile QuestionFileItem in Content ) {
        RetVal.AppendLine(QuestionFileItem.GetHeaderTextWithDetails());
      }
      return RetVal.ToString();
    }

    public virtual IQuestionFile GetFile(string filename) {
      #region === Validate parameters ===
      if ( IsInvalid ) {
        return null;
      }
      #endregion === Validate parameters ===
      string RealFilePath = Directory.GetFiles(StorageLocation, $"{filename}{TQuestionFile.QUESTION_FILE_EXTENSION}", SearchOption.AllDirectories).FirstOrDefault();
      if ( RealFilePath == null ) {
        return null;
      }
      return new TQuestionFile(RealFilePath);
    }

  }
}
