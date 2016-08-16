﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskMeLib {
  public partial class TRepository : TXmlBase, IDisposable {

    #region --- Public properties -----------------------------------------------------------------
    public List<IFolder> SubFolders { get; set; } = new List<IFolder>();

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
    public TRepository(string location) : base() {
      StorageLocation = location;
    }

    public void Dispose() {
      Items.Clear();
    }
    #endregion --- Constructor(s) -----------------------------------------------------------------

    #region --- Converters ------------------------------------------------------------------------
    public override string ToString() {
      StringBuilder RetVal = new StringBuilder();
      foreach (IFolder FolderItem in SubFolders) {
        RetVal.AppendLine(FolderItem.ToString());
      }
      return RetVal.ToString();
    }
    #endregion --- Converters ---------------------------------------------------------------------

    public virtual List<IFolder> GetContent(string category = "", string language = "", bool recurse = false) {
      SubFolders.Clear();

      #region === Validate parameters ===
      if (IsInvalid) {
        return null;
      }
      #endregion === Validate parameters ===

      string[] Folders;
      Folders = Directory.GetDirectories(StorageLocation);
      foreach (string FolderItem in Folders) {
        TFolder Folder = new TFolder(FolderItem);
        TFolderHeader Header = Folder.GetHeader();
        if ((category == "" || Header.Category.ToLower().Contains(category.ToLower()))
          && (language == "" || Header.IsLanguageMatching(language))) {
          SubFolders.Add(Folder);
        }
      }
      return SubFolders;

    }

    public virtual string GetContentList(string category = "", string language = "", bool recurse = false) {
      StringBuilder RetVal = new StringBuilder();
      IEnumerable<IFolder> Content = GetContent(category, language, recurse);
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
