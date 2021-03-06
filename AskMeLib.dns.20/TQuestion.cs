﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLTools;
using System.Xml.Linq;
using BLTools.Text;
using System.Runtime.Serialization;
using BLTools.Json;

namespace AskMeLib {

  public partial class TQuestion : TObjectBase, IQuestion {

    #region --- XML constants ----------------------------------------------------------------------------------
    public const string XML_THIS_ELEMENT = "Question";
    public const string XML_ATTRIBUTE_QUESTION_TYPE = "QuestionType";
    #endregion --- XML constants -------------------------------------------------------------------------------

    #region --- Public properties ------------------------------------------------------------------------------

    public string QuestionType { get; set; }

    public IChoiceCollection Choices { get; set; } = new TChoiceCollection();
    public int CurrentChoice { get; set; } 
    #endregion --- Public properties ---------------------------------------------------------------------------

    #region --- Constructor(s) ---------------------------------------------------------------------------------
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

    public TQuestion(XElement element) : base(element) {
      QuestionType = element.SafeReadAttribute<string>(XML_ATTRIBUTE_QUESTION_TYPE, "QCM1");
      Choices = new TChoiceCollection(element.SafeReadElement(TChoiceCollection.XML_THIS_ELEMENT));
    }

    public TQuestion(JsonObject source) : base(source) {
      QuestionType = source.SafeGetValueFirst<string>(XML_ATTRIBUTE_QUESTION_TYPE);
      Choices = new TChoiceCollection(source.SafeGetValueFirst<JsonArray>(nameof(Choices)));
    }
    #endregion --- Constructor(s) ------------------------------------------------------------------------------

    public override IJsonValue ToJson() {
      JsonObject RetVal = base.ToJson() as JsonObject;

      RetVal.Add(XML_ATTRIBUTE_QUESTION_TYPE, QuestionType);
      RetVal.Add(TChoiceCollection.XML_THIS_ELEMENT, Choices.ToJson());

      return RetVal;
    }

    public bool Render() {
      bool ReponseOk = false;

      // Display the question and get a valid answer
      do {
        Console.WriteLine(TextBox.BuildDynamicIBM(Name));
        int i = 1;
        foreach (TChoice ChoixItem in Choices.Items) {
          Console.WriteLine($"  {i++}. {ChoixItem.Name} ({(string.IsNullOrWhiteSpace(ChoixItem.Description) ? "" : ChoixItem.Description)})");
        }
        Console.WriteLine();
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
