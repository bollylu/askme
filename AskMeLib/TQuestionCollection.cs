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

namespace AskMeLib {
  public class TQuestionCollection : TXmlBase {

    public const string XML_THIS_ELEMENT = "Questions";

    #region Propriétés
    public List<TQuestion> Items { get; set; } = new List<TQuestion>();
    public int Counter { get; set; }
    #endregion Propriétés

    #region Constructeurs
    public TQuestionCollection(): base() {
    }
    
    public TQuestionCollection(string name, string description, IEnumerable<TQuestion> questions) : base() {
      Name = name;
      Description = description;
      foreach (TQuestion QuestionItem in questions) {
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
    #endregion Constructeurs

    public void Ask() {
      Counter = 0;
      Console.WriteLine(Name);
      Console.WriteLine(Description);
      Console.WriteLine("--------------------------------------------------");

      foreach (TQuestion QuestionItem in Items) {
        if (QuestionItem.Ask() == true) {
          Counter++;
          Console.WriteLine("La réponse est correcte !");
        } else {
          Console.WriteLine("Perdu !");
        }

      }
      Console.WriteLine();
      Console.WriteLine($"Vous avez réussi {Counter} question(s), votre pourcentage est de {Counter / 2f * 100f}%");
      ConsoleExtension.Pause();


    }
    
  }
}
