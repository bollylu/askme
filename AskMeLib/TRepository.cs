using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskMeLib {
  public partial class TRepository : TXmlBase, IDisposable {

    #region --- Public properties -----------------------------------------------------------------
    public string Location { get; set; }
    public List<IQuestionFile> Items { get; set; } = new List<IQuestionFile>();

    public bool IsInvalid {
      get {
        if (string.IsNullOrWhiteSpace(Location)) {
          return true;
        }
        if (!Directory.Exists(Location)) {
          return true;
        }
        return false;
      }
    }
    #endregion --- Public properties --------------------------------------------------------------

    #region --- Constructor(s) --------------------------------------------------------------------
    public TRepository(string location) : base() {
      Location = location;
    }

    public void Dispose() {
      Items.Clear();
    }
    #endregion --- Constructor(s) -----------------------------------------------------------------

    #region --- Converters ------------------------------------------------------------------------
    public override string ToString() {
      StringBuilder RetVal = new StringBuilder();
      foreach (TQuestionFile QuestionFileItem in Items) {
        RetVal.AppendLine(QuestionFileItem.ToString());
      }
      return RetVal.ToString();
    }
    #endregion --- Converters ---------------------------------------------------------------------

    public virtual List<IQuestionFile> GetContent(string category = "", string language = "", bool recurse = false) {
      Items.Clear();

      #region === Validate parameters ===
      if (IsInvalid) {
        return null;
      }
      #endregion === Validate parameters ===

      string[] Files;
      if (recurse) {
        Files = Directory.GetFiles(Location, $"*{TQuestionFile.QUESTION_FILE_EXTENSION}", SearchOption.TopDirectoryOnly);
      } else {
        Files = Directory.GetFiles(Location, $"*{TQuestionFile.QUESTION_FILE_EXTENSION}", SearchOption.AllDirectories);
      }
      foreach (string FileItem in Files) {
        IQuestionFile TempQuestionFile = new TQuestionFile(FileItem);
        if ((category == "" || TempQuestionFile.Header.Category.ToLower().Contains(category.ToLower()))
          && (language == "" || TempQuestionFile.IsLanguageMatching(language))) {
          Items.Add(TempQuestionFile);
        }
      }
      return Items;

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
      string RealFilePath = Directory.GetFiles(Location, $"{filename}{TQuestionFile.QUESTION_FILE_EXTENSION}", SearchOption.AllDirectories).FirstOrDefault();
      if (RealFilePath == null) {
        return null;
      }
      return new TQuestionFile(RealFilePath);
    }

  }
}
