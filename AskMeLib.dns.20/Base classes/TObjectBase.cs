﻿using BLTools;
using BLTools.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Runtime.Serialization;
using System.IO;

namespace AskMeLib {

  public partial class TObjectBase : TNotificationBase, IToXml, IObjectBase {

    #region --- XML constants ----------------------------------------------------------------------------------
    public const string XML_ATTRIBUTE_ID = "Id";
    public const string XML_ATTRIBUTE_NAME = "Name";
    public const string XML_ATTRIBUTE_DESCRIPTION = "Description";
    #endregion --- XML constants -------------------------------------------------------------------------------

    #region --- Public properties ------------------------------------------------------------------------------
    public string StorageLocation { get; set; }

    public string Id { get; protected set; }
    public virtual string Name { get; set; }
    public virtual string Description { get; set; }
    #endregion --- Public properties ---------------------------------------------------------------------------

    #region --- Constructor(s) ---------------------------------------------------------------------------------
    public TObjectBase() {
      Id = "";
      Name = "";
      Description = "";
    }

    public TObjectBase(XElement element) {
      Id = element.SafeReadAttribute<string>(XML_ATTRIBUTE_ID, "");
      Name = element.SafeReadAttribute<string>(XML_ATTRIBUTE_NAME, "");
      Description = element.SafeReadAttribute<string>(XML_ATTRIBUTE_DESCRIPTION, "");
    }

    public TObjectBase(IObjectBase xmlBase) {
      Id = xmlBase.Id;
      Name = xmlBase.Name;
      Description = xmlBase.Description;
    }

    public TObjectBase(JsonObject jsonObject) {
      Id = jsonObject.SafeGetValueFirst<string>(nameof(Id));
      Name = jsonObject.SafeGetValueFirst<string>(nameof(Name));
      Description = jsonObject.SafeGetValueFirst<string>(nameof(Description));
    }
    #endregion --- Constructor(s) ------------------------------------------------------------------------------

    #region --- Converters -------------------------------------------------------------------------------------
    public virtual XElement ToXml() {
      throw new NotImplementedException();
    }

    public virtual XElement ToXml(string name) {
      XElement RetVal = new XElement(name);
      RetVal.SetAttributeValue(XML_ATTRIBUTE_ID, Id);
      RetVal.SetAttributeValue(XML_ATTRIBUTE_NAME, Name);
      RetVal.SetAttributeValue(XML_ATTRIBUTE_DESCRIPTION, Description);
      return RetVal;
    }

    public virtual XElement ToXml(XName name) {
      XElement RetVal = new XElement(name);
      RetVal.SetAttributeValue(XML_ATTRIBUTE_ID, Id);
      RetVal.SetAttributeValue(XML_ATTRIBUTE_NAME, Name);
      RetVal.SetAttributeValue(XML_ATTRIBUTE_DESCRIPTION, Description);
      return RetVal;
    }

    public virtual IJsonValue ToJson() {
      JsonObject RetVal = new JsonObject(new JsonPair(XML_ATTRIBUTE_ID, Id));
      RetVal.Add(new JsonPair(XML_ATTRIBUTE_NAME, Name));
      RetVal.Add(new JsonPair(XML_ATTRIBUTE_DESCRIPTION, Description));
      return RetVal;

    }
    #endregion --- Converters -------------------------------------------------------------------------------------

    #region --- Xml IO -----------------------------------------------------------------------------------------
    public virtual bool SaveXml(string storageLocation = "") {
      #region Validate parameters
      if ( !string.IsNullOrWhiteSpace(storageLocation) ) {
        StorageLocation = storageLocation;
      }
      #endregion Validate parameters

      XDocument XmlFile = new XDocument();
      XmlFile.Declaration = new XDeclaration("1.0", Encoding.UTF8.EncodingName, "true");
      XmlFile.Add(this.ToXml());
      try {
        NotifyProgress("Saving data...");
        XmlFile.Save(StorageLocation);
        NotifyProgress("Save successful");
        return true;
      } catch ( Exception ex ) {
        NotifyError($"Unable to save information to file {StorageLocation} : {ex.Message}", ErrorLevel.Error);
        NotifyProgress("SaveXml failed");
        return false;
      }
    }

    public virtual XElement LoadXml(string storageLocation = "") {
      string CurrentStorageLocation;
      #region Validate parameters
      if ( !string.IsNullOrWhiteSpace(storageLocation) ) {
        CurrentStorageLocation = storageLocation;
      } else {
        CurrentStorageLocation = StorageLocation;
      }

      if ( !File.Exists(CurrentStorageLocation) ) {
        NotifyError($"Unable to read information from file {CurrentStorageLocation} : incorrect or missing filename", ErrorLevel.Error);
        return null;
      }
      #endregion Validate parameters
      XDocument XmlFile;
      try {
        NotifyProgress("Reading file content...");
        XmlFile = XDocument.Load(CurrentStorageLocation);

        NotifyProgress("Parsing content...");
        XElement Root = XmlFile.Root;
        if ( Root == null ) {
          NotifyError("unable to read config file content");
          return null;
        }

        NotifyProgress("LoadXml Sucessfull");
        return Root;
      } catch ( Exception ex ) {
        NotifyError($"Unable to read information from file {CurrentStorageLocation} : {ex.Message}", ErrorLevel.Error);
        NotifyProgress("LoadXml failed");
        return null;
      }
    }
    #endregion --- Xml IO --------------------------------------------------------------------------------------
  }

}
