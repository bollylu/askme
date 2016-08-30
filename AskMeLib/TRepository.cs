using BLTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AskMeLib {
  public partial class TRepository : TXmlBase, IRepository {

    public const string REPOSITORY_HEADER_FILENAME = "repository.xml";
    public const string XML_THIS_ELEMENT = "Repository";
    public const string XML_ATTRIBUTE_QCM_FOLDER = "QcmFolder";
    public const string XML_ATTRIBUTE_DESC_FOLDER = "DescFolder";

    #region --- Public properties -----------------------------------------------------------------
    public string HeaderLocation { get; private set; }
    public string DataFolder { get; set; }
    public string DescFolder { get; set; }

    public List<IQuestionFile> QFiles { get; set; } = new List<IQuestionFile>();

    public bool IsInvalid {
      get {
        if (string.IsNullOrWhiteSpace(StorageLocation)) {
          return true;
        }
        if (!Directory.Exists(StorageLocation)) {
          return true;
        }
        return false;
      }
    }
    #endregion --- Public properties --------------------------------------------------------------

    #region --- Constructor(s) --------------------------------------------------------------------
    public TRepository(string folder) : base() {
      StorageLocation = folder;
      HeaderLocation = Path.Combine(folder, REPOSITORY_HEADER_FILENAME);
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
      RetVal.Append($", Data = {DataFolder}");
      RetVal.Append($", Rich = {DescFolder}");
      RetVal.Append($", Content = {QFiles.Count()} files");
      return RetVal.ToString();
    }
    #endregion --- Converters ---------------------------------------------------------------------

    public bool Open() {
      if (IsInvalid) {
        return false;
      }

      try {
        XElement Header = LoadXml(HeaderLocation).SafeReadElement(TRepository.XML_THIS_ELEMENT);
        if (Header == null) {
          return false;
        }

        Name = Header.SafeReadAttribute<string>(XML_ATTRIBUTE_NAME, "");
        Description = Header.SafeReadAttribute<string>(XML_ATTRIBUTE_DESCRIPTION, "");
        DataFolder = Header.SafeReadAttribute<string>(XML_ATTRIBUTE_QCM_FOLDER, "");
        DescFolder = Header.SafeReadAttribute<String>(XML_ATTRIBUTE_DESC_FOLDER, "");

        return true;
      } catch {
        return false;
      }
    }

    public virtual List<IQuestionFile> GetContent(string category = "", string language = "", bool recurse = false) {
      QFiles.Clear();

      if (string.IsNullOrWhiteSpace(DataFolder)) {
        return QFiles;
      }

      string CurrentDataFolder = Path.Combine(StorageLocation, DataFolder);

      if (!Directory.Exists(CurrentDataFolder)) {
        return QFiles;
      }

      foreach (string FileItem in Directory.GetFiles(CurrentDataFolder, $"*{TQuestionFile.QUESTION_FILE_EXTENSION}", SearchOption.AllDirectories)) {
        TQuestionFile TempFile = new TQuestionFile(FileItem);
        if ((category == "" || TempFile.Header.Category.ToLower().Contains(category.ToLower()))
          && (language == "" || TempFile.Header.IsLanguageMatching(language))) {
          QFiles.Add(TempFile);
        }
      }
      return QFiles;

    }

    public virtual string GetContentList(string category = "", string language = "", bool recurse = false) {
      StringBuilder RetVal = new StringBuilder();
      IEnumerable<IQuestionFile> Content = GetContent(category, language, recurse);
      if (Content == null || Content.Count() == 0) {
        return "";
      }
      foreach (IQuestionFile QuestionFileItem in Content) {
        RetVal.AppendLine(QuestionFileItem.GetHeaderTextWithDetails());
      }
      return RetVal.ToString();
    }

    public virtual IQuestionFile GetFile(string filename) {
      #region === Validate parameters ===
      if (IsInvalid) {
        return null;
      }
      #endregion === Validate parameters ===
      string RealFilePath = Directory.GetFiles(StorageLocation, $"{filename}{TQuestionFile.QUESTION_FILE_EXTENSION}", SearchOption.AllDirectories).FirstOrDefault();
      if (RealFilePath == null) {
        return null;
      }
      return new TQuestionFile(RealFilePath);
    }

  }
}
