﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AskMeLib
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TXmlBase", Namespace="http://schemas.datacontract.org/2004/07/AskMeLib")]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(AskMeLib.TQuestionFileHeader))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(AskMeLib.TQuestionCollection))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(AskMeLib.TQuestion))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(AskMeLib.TChoiceCollection))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(AskMeLib.TChoice))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(AskMeLib.TQuestionFile))]
    public partial class TXmlBase : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string DescriptionField;
        
        private string NameField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description
        {
            get
            {
                return this.DescriptionField;
            }
            set
            {
                this.DescriptionField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TQuestionFileHeader", Namespace="http://schemas.datacontract.org/2004/07/AskMeLib")]
    public partial class TQuestionFileHeader : AskMeLib.TXmlBase
    {
        
        private string CategoryField;
        
        private string CreatedByField;
        
        private System.DateTime CreationTimeField;
        
        private AskMeLib.TQuestionCollection[] ItemsField;
        
        private string LanguageField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Category
        {
            get
            {
                return this.CategoryField;
            }
            set
            {
                this.CategoryField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CreatedBy
        {
            get
            {
                return this.CreatedByField;
            }
            set
            {
                this.CreatedByField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime CreationTime
        {
            get
            {
                return this.CreationTimeField;
            }
            set
            {
                this.CreationTimeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public AskMeLib.TQuestionCollection[] Items
        {
            get
            {
                return this.ItemsField;
            }
            set
            {
                this.ItemsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Language
        {
            get
            {
                return this.LanguageField;
            }
            set
            {
                this.LanguageField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TQuestionCollection", Namespace="http://schemas.datacontract.org/2004/07/AskMeLib")]
    public partial class TQuestionCollection : AskMeLib.TXmlBase
    {
        
        private AskMeLib.TQuestion[] ItemsField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public AskMeLib.TQuestion[] Items
        {
            get
            {
                return this.ItemsField;
            }
            set
            {
                this.ItemsField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TQuestion", Namespace="http://schemas.datacontract.org/2004/07/AskMeLib")]
    public partial class TQuestion : AskMeLib.TXmlBase
    {
        
        private AskMeLib.TChoiceCollection ChoicesField;
        
        private string QuestionTypeField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public AskMeLib.TChoiceCollection Choices
        {
            get
            {
                return this.ChoicesField;
            }
            set
            {
                this.ChoicesField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string QuestionType
        {
            get
            {
                return this.QuestionTypeField;
            }
            set
            {
                this.QuestionTypeField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TChoiceCollection", Namespace="http://schemas.datacontract.org/2004/07/AskMeLib")]
    public partial class TChoiceCollection : AskMeLib.TXmlBase
    {
        
        private AskMeLib.TChoice[] ItemsField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public AskMeLib.TChoice[] Items
        {
            get
            {
                return this.ItemsField;
            }
            set
            {
                this.ItemsField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TChoice", Namespace="http://schemas.datacontract.org/2004/07/AskMeLib")]
    public partial class TChoice : AskMeLib.TXmlBase
    {
        
        private bool IsCorrectField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsCorrect
        {
            get
            {
                return this.IsCorrectField;
            }
            set
            {
                this.IsCorrectField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TQuestionFile", Namespace="http://schemas.datacontract.org/2004/07/AskMeLib")]
    public partial class TQuestionFile : AskMeLib.TXmlBase
    {
        
        private AskMeLib.TQuestionFileHeader HeaderField;
        
        private AskMeLib.TQuestionCollection[] ItemsField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public AskMeLib.TQuestionFileHeader Header
        {
            get
            {
                return this.HeaderField;
            }
            set
            {
                this.HeaderField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public AskMeLib.TQuestionCollection[] Items
        {
            get
            {
                return this.ItemsField;
            }
            set
            {
                this.ItemsField = value;
            }
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName="IAskMeWebService")]
public interface IAskMeWebService
{
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAskMeWebService/GetRepositoryList", ReplyAction="http://tempuri.org/IAskMeWebService/GetRepositoryListResponse")]
    string GetRepositoryList();
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAskMeWebService/GetRepositoryList", ReplyAction="http://tempuri.org/IAskMeWebService/GetRepositoryListResponse")]
    System.Threading.Tasks.Task<string> GetRepositoryListAsync();
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAskMeWebService/GetQuestionFile", ReplyAction="http://tempuri.org/IAskMeWebService/GetQuestionFileResponse")]
    AskMeLib.TQuestionFile GetQuestionFile(string filename);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAskMeWebService/GetQuestionFile", ReplyAction="http://tempuri.org/IAskMeWebService/GetQuestionFileResponse")]
    System.Threading.Tasks.Task<AskMeLib.TQuestionFile> GetQuestionFileAsync(string filename);
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface IAskMeWebServiceChannel : IAskMeWebService, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class AskMeWebServiceClient : System.ServiceModel.ClientBase<IAskMeWebService>, IAskMeWebService
{
    
    public AskMeWebServiceClient()
    {
    }
    
    public AskMeWebServiceClient(string endpointConfigurationName) : 
            base(endpointConfigurationName)
    {
    }
    
    public AskMeWebServiceClient(string endpointConfigurationName, string remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public AskMeWebServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public AskMeWebServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(binding, remoteAddress)
    {
    }
    
    public string GetRepositoryList()
    {
        return base.Channel.GetRepositoryList();
    }
    
    public System.Threading.Tasks.Task<string> GetRepositoryListAsync()
    {
        return base.Channel.GetRepositoryListAsync();
    }
    
    public AskMeLib.TQuestionFile GetQuestionFile(string filename)
    {
        return base.Channel.GetQuestionFile(filename);
    }
    
    public System.Threading.Tasks.Task<AskMeLib.TQuestionFile> GetQuestionFileAsync(string filename)
    {
        return base.Channel.GetQuestionFileAsync(filename);
    }
}
