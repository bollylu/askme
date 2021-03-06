﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLTools;
using System.Xml.Linq;
using System.Runtime.Serialization;
using BLTools.Json;

namespace AskMeLib {

  public partial class TChoice : TObjectBase, IChoice {

    #region --- XML constants ----------------------------------------------------------------------------------
    public const string XML_THIS_ELEMENT = "Choice";
    public const string XML_ATTRIBUTE_IS_CORRECT = "IsCorrect";
    #endregion --- XML constants -------------------------------------------------------------------------------

    #region --- Public properties ------------------------------------------------------------------------------
    public bool IsCorrect { get; set; } = false;
    #endregion --- Public properties ---------------------------------------------------------------------------

    #region --- Constructor(s) ---------------------------------------------------------------------------------
    public TChoice() : base() {
    }
    public TChoice(string title) : base() {
      Name = title;
    }
    public TChoice(string title, string description, bool isCorrect) : base() {
      Name = title;
      Description = description;
      IsCorrect = isCorrect;
    }

    public TChoice(XElement element) : base(element) {
      IsCorrect = element.SafeReadAttribute<bool>(XML_ATTRIBUTE_IS_CORRECT, false);
    }  

    public TChoice(JsonObject source) : base(source) {
      if ( source == null ) {
        return;
      }
      IsCorrect = source.SafeGetValueFirst<bool>(XML_ATTRIBUTE_IS_CORRECT);
    }
    #endregion --- Constructor(s) ------------------------------------------------------------------------------

    public override IJsonValue ToJson() {
      JsonObject RetVal = base.ToJson() as JsonObject;

      RetVal.Add(XML_ATTRIBUTE_IS_CORRECT, IsCorrect);

      return RetVal;
    }
  }
}
