using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskMeLib {
  public class TRepository : TXmlBase, IDisposable {

    public string Location { get; set; }
    public List<TQuestionFile> Items { get; set; } = new List<TQuestionFile>();

    public TRepository(string location) : base() {
      Location = location;
    }

    public void Dispose() {
      Items.Clear();
    }

    public override string ToString() {
      StringBuilder RetVal = new StringBuilder();
      foreach (TQuestionFile QuestionFileItem in Items) {
        RetVal.AppendLine(QuestionFileItem.ToString());
      }
      return RetVal.ToString();
    }

    public virtual List<TQuestionFile> GetContent(string category = "", bool recurse = false) {
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
        TQuestionFile TempQuestion = new TQuestionFile(FileItem);
        if (TempQuestion.Header.Category.ToLower().Contains(category.ToLower())) {
          Items.Add(TempQuestion);
        }
      }
      return Items;

    }
    public string GetContentList(string category = "", bool recurse = false) {
      StringBuilder RetVal = new StringBuilder();
      foreach (TQuestionFile QuestionFileItem in GetContent(category, recurse)) {
        RetVal.AppendLine(QuestionFileItem.HeaderTextWithDetails());
      }
      return RetVal.ToString();
    }

    
  }
}
