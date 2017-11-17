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
using System.Runtime.Serialization;
using BLTools.Json;

namespace AskMeLib {

  public partial class TQuestionCollection : TObjectBase, IQuestionCollection {

    #region --- XML constants ----------------------------------------------------------------------------------
    public const string XML_THIS_ELEMENT = "Questions";
    public static readonly string XML_ATTRIBUTE_COUNTER = nameof(Counter);
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
      foreach ( IQuestion QuestionItem in questions ) {
        Items.Add(QuestionItem);
      }
    }
    public TQuestionCollection(XElement element) : base(element) {
      if ( element.Elements(TQuestion.XML_THIS_ELEMENT).Count() > 0 ) {
        foreach ( XElement QuestionItem in element.Elements(TQuestion.XML_THIS_ELEMENT) ) {
          Items.Add(new TQuestion(QuestionItem));
        }
      }
    }

    public TQuestionCollection(IQuestionCollection questionCollection) : base(questionCollection) {
      foreach ( IQuestion QuestionItem in questionCollection.Items ) {
        Items.Add(QuestionItem);
      }
      Counter = questionCollection.Counter;
    }

    public TQuestionCollection(JsonObject source) {
      foreach ( JsonObject QuestionItem in source.SafeGetValueFirst<JsonArray>(XML_THIS_ELEMENT) ) {
        Items.Add(new TQuestion(QuestionItem));
      }
      Counter = source.SafeGetValueFirst<int>(XML_ATTRIBUTE_COUNTER, -1);
    }

    #endregion --- Constructeurs ------------------------------------------------------------------

    public override IJsonValue ToJson() {
      JsonObject RetVal = base.ToJson() as JsonObject;
      JsonArray Questions = new JsonArray();
      foreach ( IQuestion QuestionItem in Items ) {
        Questions.Add(QuestionItem.ToJson());
      }
      RetVal.Add(XML_THIS_ELEMENT, Questions);
      RetVal.Add(XML_ATTRIBUTE_COUNTER, Counter);
      return RetVal;
    }

    public void Render() {
      Counter = 0;
      Console.WriteLine($"Collection : {Name}");
      Console.WriteLine(Description);
      Console.WriteLine(new string('-', Description.Length));

      foreach ( IQuestion QuestionItem in Items ) {
        if ( QuestionItem.Render() == true ) {
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
