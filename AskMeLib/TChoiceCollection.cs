﻿using System;
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
  public class TChoiceCollection : TXmlBase {

    #region --- XML constants ----------------------------------------------------------------------------------
    public const string XML_THIS_ELEMENT = "Choices"; 
    #endregion --- XML constants -------------------------------------------------------------------------------

    #region --- Public properties ------------------------------------------------------------------------------
    public List<TChoice> Items { get; set; } = new List<TChoice>();
    #endregion --- Public properties ---------------------------------------------------------------------------

    #region --- Constructor(s) ---------------------------------------------------------------------------------
    public TChoiceCollection() : base() {
    }

    public TChoiceCollection(XElement element) : base(element) {
      if (element.Name != XName.Get(XML_THIS_ELEMENT)) {
        return;
      }
      foreach (XElement ChoiceItem in element.Elements(TChoice.XML_THIS_ELEMENT)) {
        Items.Add(new TChoice(ChoiceItem));
      }
    } 
    #endregion --- Constructor(s) ------------------------------------------------------------------------------

  }
}