﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLTools;
using System.Xml.Linq;

namespace AskMeLib {
  public class TQuestion : TXmlBase {

    public const string XML_THIS_ELEMENT = "Question";
    public const string XML_ATTRIBUTE_QUESTION_TYPE = "QuestionType";

    #region Propriétés
    public string QuestionType { get; set; }
    public TChoiceCollection Choices { get; set; } = new TChoiceCollection();
    public int CurrentChoice { get; set; }
    #endregion Propriétés

    #region Constructors
    public TQuestion() : base() {
      QuestionType = "QCM1";
    }
    public TQuestion(string title) : base() {
      Name = title;
      QuestionType = "QCM1";
    }
    public TQuestion(string title, IEnumerable<TChoice> choices) : base() {
      Name = title;
      foreach (TChoice ChoiceItem in choices) {
        Choices.Items.Add(ChoiceItem);
      }
      QuestionType = "QCM1";
    }

    public TQuestion(XElement element) : base() {
      QuestionType = element.SafeReadAttribute<string>(XML_ATTRIBUTE_QUESTION_TYPE, "QCM1");
      Choices = new TChoiceCollection(element.SafeReadElement(TChoiceCollection.XML_THIS_ELEMENT));
    }
    #endregion Constructors

    public bool Ask() {
      bool ReponseOk = false;

      // Display the question and get a valid answer
      do {
        Console.WriteLine(Name);
        int i = 1;
        foreach (TChoice ChoixItem in Choices.Items) {
          Console.WriteLine($"  {i++}. {ChoixItem.Name} ({(string.IsNullOrWhiteSpace(ChoixItem.Description) ? "" : ChoixItem.Description)})");
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
      if (Choices.Items[CurrentChoice-1].IsCorrect) { 
        return true;
      } else {
        return false;
      }


    }
  }
}
