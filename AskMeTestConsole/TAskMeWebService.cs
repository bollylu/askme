﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AskMeWebService {
  using System.Runtime.Serialization;


  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
  [System.Runtime.Serialization.DataContractAttribute(Name = "TXmlBaseWCF", Namespace = "http://schemas.datacontract.org/2004/07/AskMeWebService")]
  [System.Runtime.Serialization.KnownTypeAttribute(typeof(AskMeWebService.TQuestionFileHeaderWCF))]
  [System.Runtime.Serialization.KnownTypeAttribute(typeof(AskMeWebService.TQuestionCollectionWCF))]
  [System.Runtime.Serialization.KnownTypeAttribute(typeof(AskMeWebService.TQuestionWCF))]
  [System.Runtime.Serialization.KnownTypeAttribute(typeof(AskMeWebService.TChoiceCollectionWCF))]
  [System.Runtime.Serialization.KnownTypeAttribute(typeof(AskMeWebService.TChoiceWCF))]
  [System.Runtime.Serialization.KnownTypeAttribute(typeof(AskMeWebService.TQuestionFileWCF))]
  public partial class TXmlBaseWCF : object, System.Runtime.Serialization.IExtensibleDataObject {

    private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

    private string DescriptionField;

    private string NameField;

    public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
      get {
        return this.extensionDataField;
      }
      set {
        this.extensionDataField = value;
      }
    }

    [System.Runtime.Serialization.DataMemberAttribute()]
    public string Description {
      get {
        return this.DescriptionField;
      }
      set {
        this.DescriptionField = value;
      }
    }

    [System.Runtime.Serialization.DataMemberAttribute()]
    public string Name {
      get {
        return this.NameField;
      }
      set {
        this.NameField = value;
      }
    }
  }

  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
  [System.Runtime.Serialization.DataContractAttribute(Name = "TQuestionFileHeaderWCF", Namespace = "http://schemas.datacontract.org/2004/07/AskMeWebService")]
  public partial class TQuestionFileHeaderWCF : AskMeWebService.TXmlBaseWCF {

    private string CategoryField;

    private string CreatedByField;

    private System.DateTime CreationTimeField;

    private string LanguageField;

    [System.Runtime.Serialization.DataMemberAttribute()]
    public string Category {
      get {
        return this.CategoryField;
      }
      set {
        this.CategoryField = value;
      }
    }

    [System.Runtime.Serialization.DataMemberAttribute()]
    public string CreatedBy {
      get {
        return this.CreatedByField;
      }
      set {
        this.CreatedByField = value;
      }
    }

    [System.Runtime.Serialization.DataMemberAttribute()]
    public System.DateTime CreationTime {
      get {
        return this.CreationTimeField;
      }
      set {
        this.CreationTimeField = value;
      }
    }

    [System.Runtime.Serialization.DataMemberAttribute()]
    public string Language {
      get {
        return this.LanguageField;
      }
      set {
        this.LanguageField = value;
      }
    }
  }

  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
  [System.Runtime.Serialization.DataContractAttribute(Name = "TQuestionCollectionWCF", Namespace = "http://schemas.datacontract.org/2004/07/AskMeWebService")]
  public partial class TQuestionCollectionWCF : AskMeWebService.TXmlBaseWCF {

    private AskMeWebService.TQuestionWCF[] ItemsField;

    [System.Runtime.Serialization.DataMemberAttribute()]
    public AskMeWebService.TQuestionWCF[] Items {
      get {
        return this.ItemsField;
      }
      set {
        this.ItemsField = value;
      }
    }
  }

  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
  [System.Runtime.Serialization.DataContractAttribute(Name = "TQuestionWCF", Namespace = "http://schemas.datacontract.org/2004/07/AskMeWebService")]
  public partial class TQuestionWCF : AskMeWebService.TXmlBaseWCF {

    private AskMeWebService.TChoiceCollectionWCF ChoicesField;

    private string QuestionTypeField;

    [System.Runtime.Serialization.DataMemberAttribute()]
    public AskMeWebService.TChoiceCollectionWCF Choices {
      get {
        return this.ChoicesField;
      }
      set {
        this.ChoicesField = value;
      }
    }

    [System.Runtime.Serialization.DataMemberAttribute()]
    public string QuestionType {
      get {
        return this.QuestionTypeField;
      }
      set {
        this.QuestionTypeField = value;
      }
    }
  }

  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
  [System.Runtime.Serialization.DataContractAttribute(Name = "TChoiceCollectionWCF", Namespace = "http://schemas.datacontract.org/2004/07/AskMeWebService")]
  public partial class TChoiceCollectionWCF : AskMeWebService.TXmlBaseWCF {

    private AskMeWebService.TChoiceWCF[] ItemsField;

    [System.Runtime.Serialization.DataMemberAttribute()]
    public AskMeWebService.TChoiceWCF[] Items {
      get {
        return this.ItemsField;
      }
      set {
        this.ItemsField = value;
      }
    }
  }

  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
  [System.Runtime.Serialization.DataContractAttribute(Name = "TChoiceWCF", Namespace = "http://schemas.datacontract.org/2004/07/AskMeWebService")]
  public partial class TChoiceWCF : AskMeWebService.TXmlBaseWCF {

    private bool IsCorrectField;

    [System.Runtime.Serialization.DataMemberAttribute()]
    public bool IsCorrect {
      get {
        return this.IsCorrectField;
      }
      set {
        this.IsCorrectField = value;
      }
    }
  }

  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
  [System.Runtime.Serialization.DataContractAttribute(Name = "TQuestionFileWCF", Namespace = "http://schemas.datacontract.org/2004/07/AskMeWebService")]
  public partial class TQuestionFileWCF : AskMeWebService.TXmlBaseWCF {

    private AskMeWebService.TQuestionFileHeaderWCF HeaderField;

    private AskMeWebService.TQuestionCollectionWCF[] ItemsField;

    [System.Runtime.Serialization.DataMemberAttribute()]
    public AskMeWebService.TQuestionFileHeaderWCF Header {
      get {
        return this.HeaderField;
      }
      set {
        this.HeaderField = value;
      }
    }

    [System.Runtime.Serialization.DataMemberAttribute()]
    public AskMeWebService.TQuestionCollectionWCF[] Items {
      get {
        return this.ItemsField;
      }
      set {
        this.ItemsField = value;
      }
    }
  }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName = "IAskMeWebService")]
public interface IAskMeWebService {

  [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IAskMeWebService/GetRepositoryList", ReplyAction = "http://tempuri.org/IAskMeWebService/GetRepositoryListResponse")]
  string GetRepositoryList();

  [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IAskMeWebService/GetRepositoryList", ReplyAction = "http://tempuri.org/IAskMeWebService/GetRepositoryListResponse")]
  System.Threading.Tasks.Task<string> GetRepositoryListAsync();

  [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IAskMeWebService/GetQuestionFile", ReplyAction = "http://tempuri.org/IAskMeWebService/GetQuestionFileResponse")]
  AskMeWebService.TQuestionFileWCF GetQuestionFile(string filename);

  [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IAskMeWebService/GetQuestionFile", ReplyAction = "http://tempuri.org/IAskMeWebService/GetQuestionFileResponse")]
  System.Threading.Tasks.Task<AskMeWebService.TQuestionFileWCF> GetQuestionFileAsync(string filename);
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface IAskMeWebServiceChannel : IAskMeWebService, System.ServiceModel.IClientChannel {
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class AskMeWebServiceClient : System.ServiceModel.ClientBase<IAskMeWebService>, IAskMeWebService {

  public AskMeWebServiceClient() {
  }

  public AskMeWebServiceClient(string endpointConfigurationName) :
          base(endpointConfigurationName) {
  }

  public AskMeWebServiceClient(string endpointConfigurationName, string remoteAddress) :
          base(endpointConfigurationName, remoteAddress) {
  }

  public AskMeWebServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
          base(endpointConfigurationName, remoteAddress) {
  }

  public AskMeWebServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
          base(binding, remoteAddress) {
  }

  public string GetRepositoryList() {
    return base.Channel.GetRepositoryList();
  }

  public System.Threading.Tasks.Task<string> GetRepositoryListAsync() {
    return base.Channel.GetRepositoryListAsync();
  }

  public AskMeWebService.TQuestionFileWCF GetQuestionFile(string filename) {
    return base.Channel.GetQuestionFile(filename);
  }

  public System.Threading.Tasks.Task<AskMeWebService.TQuestionFileWCF> GetQuestionFileAsync(string filename) {
    return base.Channel.GetQuestionFileAsync(filename);
  }
}
