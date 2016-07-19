using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLTools;
using System.Xml.Linq;

namespace AskMeLib {
  public class TQuestion {

    public const string XML_THIS_ELEMENT = "Question";
    public const string XML_ATTRIBUTE_TITLE = "Title";
    public const string XML_ATTRIBUTE_CORRECT_ANSWER = "CorrectAnswer";
    public const string XML_ELEMENT_CHOICE = "Choice";
    public const string XML_ATTRIBUTE_CHOICE_TEXT = "Text";

    #region Propriétés
    public string Title { get; set; }
    public List<string> Choices { get; set; }
    public int CorrectAnswer { get; set; }
    public int CurrentChoice { get; set; }
    #endregion Propriétés

    #region Constructors
    public TQuestion() {
      Title = "";
      Choices = new List<string>();
    }
    public TQuestion(string title) : this() {
      Title = title;
    }
    public TQuestion(string title, IEnumerable<string> choices, int correctAnswer) : this() {
      Title = title;
      CorrectAnswer = correctAnswer;
      foreach (string ChoiceItem in choices) {
        Choices.Add(ChoiceItem);
      }
    }

    public TQuestion(XElement element) : this () {
      Title = element.SafeReadAttribute<string>(XML_ATTRIBUTE_TITLE, "");
      CorrectAnswer = element.SafeReadAttribute<int>(XML_ATTRIBUTE_CORRECT_ANSWER, 0);
      if (element.Elements(XML_ELEMENT_CHOICE).Count()>0 ) {
        foreach(XElement ChoiceItem in element.Elements(XML_ELEMENT_CHOICE)) {
          string TempChoice = ChoiceItem.SafeReadAttribute<string>(XML_ATTRIBUTE_CHOICE_TEXT, "");
          Choices.Add(TempChoice);
        }
      }
    }
    #endregion Constructors

    public bool Ask() {
      bool ReponseOk = false;

      // Display the question and get a valid answer
      do {
        Console.WriteLine(Title);
        int i = 1;
        foreach (string ChoixItem in Choices) {
          Console.WriteLine(string.Format("  {0}. {1}", i++, ChoixItem));
        }
        Console.WriteLine("Veuillez choisir une des valeurs proposées");

        try {
          CurrentChoice = int.Parse(Console.ReadLine());
          if (CurrentChoice > 0 && CurrentChoice < i) {
            ReponseOk = true;
          }
        } catch (Exception ex) {
          Console.WriteLine(string.Format("Erreur : {0}", ex.Message));
        }
      } while (!ReponseOk);

      // Test the answer for correctness
      if (CurrentChoice == CorrectAnswer) {
        return true;
      } else {
        return false;
      }


    }
  }
}
