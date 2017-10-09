using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLTools;
using BLTools.ConsoleExtension;
using System.Xml.Linq;
using System.IO;
using System.Diagnostics;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace AskMeLib {

  public partial class TQuestionCollection : TXmlBase, IQuestionCollection {

    #region --- XML constants ----------------------------------------------------------------------------------
    public const string XML_THIS_ELEMENT = "Questions";
    #endregion --- XML constants -------------------------------------------------------------------------------

    #region --- Properties ------------------------------------------------------------------------
    public List<IQuestion> Items { get; set; } = new List<IQuestion>();
    public int Counter { get; set; }
    #endregion --- Properties ---------------------------------------------------------------------

    #region --- Constructeurs ---------------------------------------------------------------------
    public TQuestionCollection() : base() {
    }

    public TQuestionCollection(string name, string description, IEnumerable<IQuestion> questions) : base() {
      Name = name;
      Description = description;
      foreach (IQuestion QuestionItem in questions) {
        Items.Add(QuestionItem);
      }
    }
    public TQuestionCollection(XElement element) : base(element) {
      if (element.Elements(TQuestion.XML_THIS_ELEMENT).Count() > 0) {
        foreach (XElement QuestionItem in element.Elements(TQuestion.XML_THIS_ELEMENT)) {
          Items.Add(new TQuestion(QuestionItem));
        }
      }
    }

    public TQuestionCollection(IQuestionCollection questionCollection) : base(questionCollection) {
      foreach (IQuestion QuestionItem in questionCollection.Items) {
        Items.Add(QuestionItem);
      }
      Counter = questionCollection.Counter;
    }
    #endregion --- Constructeurs ------------------------------------------------------------------

    public string ToJSon() {
      StringBuilder RetVal = new StringBuilder("{[");
      foreach (IQuestion QuestionItem in Items) {
        RetVal.Append(QuestionItem.ToJSon());
        RetVal.Append(",");
      }
      RetVal.Truncate(1);
      RetVal.Append("]}");
      return RetVal.ToString();
    }

    public void Ask() {
      Counter = 0;
      Console.WriteLine($"Collection : {Name}");
      Console.WriteLine(Description);
      Console.WriteLine(new string('-', Description.Length));

      foreach (IQuestion QuestionItem in Items) {
        if (QuestionItem.Ask() == true) {
          Counter++;
          Console.WriteLine("La réponse est correcte !");
        } else {
          Console.WriteLine("Perdu !");
        }

      }
      Console.WriteLine();
      Console.WriteLine($"Vous avez réussi {Counter} question(s), votre pourcentage est de {Counter / 2f * 100f}%");

    }

  }
}
