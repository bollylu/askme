using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AskMeLib {
  public class TQuestionFile :TXmlBase {

    public const string XML_THIS_ELEMENT = "root";

    public string Location { get; set; }
    public List<TQuestionCollection> Items { get; set; } = new List<TQuestionCollection>();

    public TQuestionFile(string location) : base() {
      Location = location;
    }

    public void Load(string location = "") {
      #region Validate parameters
      if (location != "") {
        Location = location;
      }
      if (!File.Exists(Location)) {
        return;
      }
      #endregion Validate parameters

      Items.Clear();
      Trace.WriteLine($"Loading {location}");
      try {
        XDocument MyData = XDocument.Load(Location);
        XElement Root = MyData.Element(XML_THIS_ELEMENT);
        IEnumerable<XElement> QuestionCollections = Root.Elements(TQuestionCollection.XML_THIS_ELEMENT);
        if (QuestionCollections.Count() > 0) {
          foreach (XElement QuestionCollectionItem in QuestionCollections) {
            Items.Add(new TQuestionCollection(QuestionCollectionItem));
          }
        }


      } catch (Exception ex) {
        Trace.WriteLine($"Reading error : {ex.Message}");
        return;
      }
    }

    public TQuestionFileHeader ReadHeader(string location="") {
      #region Validate parameters
      if (location != "") {
        Location = location;
      }
      if (!File.Exists(Location)) {
        return null;
      }
      #endregion Validate parameters

      Trace.WriteLine($"Loading {Location}");
      TQuestionFileHeader Header;
      XDocument MyData;
      try {
        MyData = XDocument.Load(Location);
        XElement Root = MyData.Element(XML_THIS_ELEMENT);
        Header = new TQuestionFileHeader(Root.Element(TQuestionFileHeader.XML_THIS_ELEMENT));
        return Header;
      } catch (Exception ex) {
        Trace.WriteLine($"Reading error : {ex.Message}");
        return null;
      }
    }
  }
}
