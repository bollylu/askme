using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLTools.ConsoleExtension;
using AskMeLib;

namespace AskMeTestConsole {
  class Program {
    static void Main(string[] args) {
    

      const string QHenri = "Quelle est la couleur du cheval blanc d'Henri IV";
      List<string> HenriCheval = new List<string>() {
        "blanc",
        "noir",
        "vert",
        "bleu"
      };
      const string QSang = "Quelle est la proportion de globules rouges/globules blancs/plaquettes dans le sang ?";
      List<string> QBloodAnswer = new List<string>() {
        "10%, 60%, 30%",
        "70%, 25%, 5%",
        "42%, 55%, 3%",
        "1%, 1%, 98%"
      };

      TQuestion Question1 = new TQuestion();
      TQuestion Question2 = new TQuestion(QHenri);
      TQuestion Question3 = new TQuestion(QHenri, HenriCheval, 1);
      TQuestion Question4 = new TQuestion(QSang, QBloodAnswer, 2); ;

      TQuestionCollection MesQuestions = new TQuestionCollection("Mes Questions", "Questions cours EAD", new List<TQuestion>() { Question3, Question4 });
      MesQuestions.Ask();
    

      
    }
  }
}
