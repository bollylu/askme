using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLTools;
using BLTools.ConsoleExtension;
using System.Xml.Linq;
using System.IO;

namespace AskMeLib {
  public class TQuestionCollection {

    public const string XML_THIS_ELEMENT = "Questions";
    public const string XML_ATTRIBUTE_NAME = "Name";
    public const string XML_ATTRIBUTE_DESCRIPTION = "Description";


    #region Propriétés
    public string Location { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<TQuestion> Items { get; set; }
    public int Counter { get; set;}
    #endregion Propriétés


    #region Constructeurs
    public TQuestionCollection() {
      Name = "";
      Description = "";
      Items = new List<TQuestion>();
    }
    public TQuestionCollection(string location) : this() {
      Location = location;
    }
    public TQuestionCollection(string name, string description, IEnumerable<TQuestion> questions) : this() {
      Name = name;
      Description = description;
      foreach (TQuestion QuestionItem in questions) {
        Items.Add(QuestionItem);
      }
    }
    public TQuestionCollection(XElement element) {
      Initialize(element);
    }
    #endregion Constructeurs

    public void Initialize(XElement element) {
      Name = element.SafeReadAttribute<string>(XML_ATTRIBUTE_NAME, "");
      Description = element.SafeReadAttribute<string>(XML_ATTRIBUTE_DESCRIPTION, "");
      if (element.Elements(TQuestion.XML_THIS_ELEMENT).Count() > 0) {
        foreach (XElement QuestionItem in element.Elements(TQuestion.XML_THIS_ELEMENT)) {
          Items.Add(new TQuestion(QuestionItem));
        }
      }
    }

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
      Console.WriteLine(string.Format("Vous avez réussi {0} question(s), votre pourcentage est de {1}%", Counter, Counter / 2f * 100f));
      ConsoleExtension.Pause();


    }
    public void Load(string location = "") {
      if (location != "") {
        Location = location;
      } 
      if (!File.Exists(Location)) {
        return;
      }
      try {
        XDocument MyData = XDocument.Load(Location);
        XElement Root = MyData.Root;
        if (Root.Elements(TQuestionCollection.XML_THIS_ELEMENT).Count() > 0) {
          Initialize(Root.Elements(TQuestionCollection.XML_THIS_ELEMENT).First());
        }


      } catch (Exception ex) {
        Console.WriteLine($"Il y a une erreur de lecture, veuillez choisir un fichier : {ex.Message}");
        return;
      }
    }
  }
}
