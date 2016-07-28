using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskMeLib {
  public class TRepository : TXmlBase, IDisposable {

    #region --- Public properties -----------------------------------------------------------------
    public string Location { get; set; }
    public List<TQuestionFile> Items { get; set; } = new List<TQuestionFile>();
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

    public virtual List<TQuestionFile> GetContent(string category = "", string language = "", bool recurse = false) {
      Items.Clear();

      if (string.IsNullOrWhiteSpace(Location)) {
        return null;
      }
      if (!Directory.Exists(Location)) {
        return null;
      }

      string[] Files;
      if (recurse) {
        Files = Directory.GetFiles(Location, TQuestionFile.QUESTION_FILE_EXTENSION, SearchOption.TopDirectoryOnly);
      } else {
        Files = Directory.GetFiles(Location, TQuestionFile.QUESTION_FILE_EXTENSION, SearchOption.AllDirectories);
      }
      foreach (string FileItem in Files) {
        TQuestionFile TempQuestionFile = new TQuestionFile(FileItem);
        //TempQuestionFile.Load();
        if (TempQuestionFile.Header.Category.ToLower().Contains(category.ToLower()) && TempQuestionFile.IsLanguageOk(language)) {
          Items.Add(TempQuestionFile);
        }
      }
      return Items;

    }
    public string GetContentList(string category = "", string language="", bool recurse = false) {
      StringBuilder RetVal = new StringBuilder();
      foreach (TQuestionFile QuestionFileItem in GetContent(category, language, recurse)) {
        RetVal.AppendLine(QuestionFileItem.GetHeaderTextWithDetails());
      }
      return RetVal.ToString();
    }


  }
}
