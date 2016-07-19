using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLTools.ConsoleExtension;

namespace AskMeLib {
  public class TQuestionCollection {

    #region Propriétés
    public string Name { get; set; }
    public string Description { get; set; }
    public List<TQuestion> Items { get; set; }
    public int Counter { get; set;}
    #endregion Propriétés


    public TQuestionCollection() {
      Name = "";
      Description = "";
      Items = new List<TQuestion>();
    }
    public TQuestionCollection(string name, string description, IEnumerable<TQuestion> questions) : this() {
      Name = name;
      Description = description;
      foreach (TQuestion QuestionItem in questions) {
        Items.Add(QuestionItem);
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
  }
}
