﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsoleApp8.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TagValue", Namespace="http://schemas.datacontract.org/2004/07/Sitel.TedisNet.Common.LocalEntities.State" +
        "")]
    [System.SerializableAttribute()]
    public partial class TagValue : ConsoleApp8.ServiceReference1.BaseEntity {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsTagValueChangeHiddenField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> QualityDetailIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> QualityIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> QualitySourceIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> SourceTimestampField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int TagIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> UpdateTimestampField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<bool> ValueBoolField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> ValueEnumIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<double> ValueFloatField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> ValueIntField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ValueStrField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsTagValueChangeHidden {
            get {
                return this.IsTagValueChangeHiddenField;
            }
            set {
                if ((this.IsTagValueChangeHiddenField.Equals(value) != true)) {
                    this.IsTagValueChangeHiddenField = value;
                    this.RaisePropertyChanged("IsTagValueChangeHidden");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> QualityDetailId {
            get {
                return this.QualityDetailIdField;
            }
            set {
                if ((this.QualityDetailIdField.Equals(value) != true)) {
                    this.QualityDetailIdField = value;
                    this.RaisePropertyChanged("QualityDetailId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> QualityId {
            get {
                return this.QualityIdField;
            }
            set {
                if ((this.QualityIdField.Equals(value) != true)) {
                    this.QualityIdField = value;
                    this.RaisePropertyChanged("QualityId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> QualitySourceId {
            get {
                return this.QualitySourceIdField;
            }
            set {
                if ((this.QualitySourceIdField.Equals(value) != true)) {
                    this.QualitySourceIdField = value;
                    this.RaisePropertyChanged("QualitySourceId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> SourceTimestamp {
            get {
                return this.SourceTimestampField;
            }
            set {
                if ((this.SourceTimestampField.Equals(value) != true)) {
                    this.SourceTimestampField = value;
                    this.RaisePropertyChanged("SourceTimestamp");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int TagId {
            get {
                return this.TagIdField;
            }
            set {
                if ((this.TagIdField.Equals(value) != true)) {
                    this.TagIdField = value;
                    this.RaisePropertyChanged("TagId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> UpdateTimestamp {
            get {
                return this.UpdateTimestampField;
            }
            set {
                if ((this.UpdateTimestampField.Equals(value) != true)) {
                    this.UpdateTimestampField = value;
                    this.RaisePropertyChanged("UpdateTimestamp");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<bool> ValueBool {
            get {
                return this.ValueBoolField;
            }
            set {
                if ((this.ValueBoolField.Equals(value) != true)) {
                    this.ValueBoolField = value;
                    this.RaisePropertyChanged("ValueBool");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> ValueEnumId {
            get {
                return this.ValueEnumIdField;
            }
            set {
                if ((this.ValueEnumIdField.Equals(value) != true)) {
                    this.ValueEnumIdField = value;
                    this.RaisePropertyChanged("ValueEnumId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<double> ValueFloat {
            get {
                return this.ValueFloatField;
            }
            set {
                if ((this.ValueFloatField.Equals(value) != true)) {
                    this.ValueFloatField = value;
                    this.RaisePropertyChanged("ValueFloat");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> ValueInt {
            get {
                return this.ValueIntField;
            }
            set {
                if ((this.ValueIntField.Equals(value) != true)) {
                    this.ValueIntField = value;
                    this.RaisePropertyChanged("ValueInt");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ValueStr {
            get {
                return this.ValueStrField;
            }
            set {
                if ((object.ReferenceEquals(this.ValueStrField, value) != true)) {
                    this.ValueStrField = value;
                    this.RaisePropertyChanged("ValueStr");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BaseEntity", Namespace="http://schemas.datacontract.org/2004/07/Sitel.TedisNet.Common.LocalEntities.Base")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ConsoleApp8.ServiceReference1.TagValue))]
    public partial class BaseEntity : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://Sitel.TedisNet.System1.SystemDataService", ConfigurationName="ServiceReference1.IStateService")]
    public interface IStateService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Sitel.TedisNet.System1.SystemDataService/IStateService/GetStateEntitySet", ReplyAction="http://Sitel.TedisNet.System1.SystemDataService/IStateService/GetStateEntitySetRe" +
            "sponse")]
        ConsoleApp8.ServiceReference1.GetStateEntitySetResponse GetStateEntitySet(ConsoleApp8.ServiceReference1.GetStateEntitySetRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Sitel.TedisNet.System1.SystemDataService/IStateService/GetTagValues", ReplyAction="http://Sitel.TedisNet.System1.SystemDataService/IStateService/GetTagValuesRespons" +
            "e")]
        ConsoleApp8.ServiceReference1.GetTagValuesResponse GetTagValues(ConsoleApp8.ServiceReference1.GetTagValuesRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Sitel.TedisNet.System1.SystemDataService/IStateService/GetTagValuesBetween" +
            "Dates", ReplyAction="http://Sitel.TedisNet.System1.SystemDataService/IStateService/GetTagValuesBetween" +
            "DatesResponse")]
        ConsoleApp8.ServiceReference1.GetTagValuesBetweenDatesResponse GetTagValuesBetweenDates(ConsoleApp8.ServiceReference1.GetTagValuesBetweenDatesRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Sitel.TedisNet.System1.SystemDataService/IStateService/GetOldestStateEntit" +
            "ySetId", ReplyAction="http://Sitel.TedisNet.System1.SystemDataService/IStateService/GetOldestStateEntit" +
            "ySetIdResponse")]
        ConsoleApp8.ServiceReference1.GetOldestStateEntitySetIdResponse GetOldestStateEntitySetId(ConsoleApp8.ServiceReference1.GetOldestStateEntitySetIdRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetStateEntitySet", WrapperNamespace="http://Sitel.TedisNet.System1.SystemDataService", IsWrapped=true)]
    public partial class GetStateEntitySetRequest {
        
        public GetStateEntitySetRequest() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetStateEntitySetResponse", WrapperNamespace="http://Sitel.TedisNet.System1.SystemDataService", IsWrapped=true)]
    public partial class GetStateEntitySetResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://Sitel.TedisNet.System1.SystemDataService", Order=0)]
        public int GetStateEntitySetResult;
        
        public GetStateEntitySetResponse() {
        }
        
        public GetStateEntitySetResponse(int GetStateEntitySetResult) {
            this.GetStateEntitySetResult = GetStateEntitySetResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetTagValues", WrapperNamespace="http://Sitel.TedisNet.System1.SystemDataService", IsWrapped=true)]
    public partial class GetTagValuesRequest {
        
        public GetTagValuesRequest() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetTagValuesResponse", WrapperNamespace="http://Sitel.TedisNet.System1.SystemDataService", IsWrapped=true)]
    public partial class GetTagValuesResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://Sitel.TedisNet.System1.SystemDataService", Order=0)]
        public bool GetTagValuesResult;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://Sitel.TedisNet.System1.SystemDataService", Order=1)]
        public System.Collections.Generic.Dictionary<int, ConsoleApp8.ServiceReference1.TagValue> tagValues;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://Sitel.TedisNet.System1.SystemDataService", Order=2)]
        public string message;
        
        public GetTagValuesResponse() {
        }
        
        public GetTagValuesResponse(bool GetTagValuesResult, System.Collections.Generic.Dictionary<int, ConsoleApp8.ServiceReference1.TagValue> tagValues, string message) {
            this.GetTagValuesResult = GetTagValuesResult;
            this.tagValues = tagValues;
            this.message = message;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetTagValuesBetweenDates", WrapperNamespace="http://Sitel.TedisNet.System1.SystemDataService", IsWrapped=true)]
    public partial class GetTagValuesBetweenDatesRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://Sitel.TedisNet.System1.SystemDataService", Order=0)]
        public int tagId;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://Sitel.TedisNet.System1.SystemDataService", Order=1)]
        public System.DateTime startDate;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://Sitel.TedisNet.System1.SystemDataService", Order=2)]
        public System.DateTime endDate;
        
        public GetTagValuesBetweenDatesRequest() {
        }
        
        public GetTagValuesBetweenDatesRequest(int tagId, System.DateTime startDate, System.DateTime endDate) {
            this.tagId = tagId;
            this.startDate = startDate;
            this.endDate = endDate;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetTagValuesBetweenDatesResponse", WrapperNamespace="http://Sitel.TedisNet.System1.SystemDataService", IsWrapped=true)]
    public partial class GetTagValuesBetweenDatesResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://Sitel.TedisNet.System1.SystemDataService", Order=0)]
        public bool GetTagValuesBetweenDatesResult;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://Sitel.TedisNet.System1.SystemDataService", Order=1)]
        public ConsoleApp8.ServiceReference1.TagValue[] tagValues;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://Sitel.TedisNet.System1.SystemDataService", Order=2)]
        public string message;
        
        public GetTagValuesBetweenDatesResponse() {
        }
        
        public GetTagValuesBetweenDatesResponse(bool GetTagValuesBetweenDatesResult, ConsoleApp8.ServiceReference1.TagValue[] tagValues, string message) {
            this.GetTagValuesBetweenDatesResult = GetTagValuesBetweenDatesResult;
            this.tagValues = tagValues;
            this.message = message;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetOldestStateEntitySetId", WrapperNamespace="http://Sitel.TedisNet.System1.SystemDataService", IsWrapped=true)]
    public partial class GetOldestStateEntitySetIdRequest {
        
        public GetOldestStateEntitySetIdRequest() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetOldestStateEntitySetIdResponse", WrapperNamespace="http://Sitel.TedisNet.System1.SystemDataService", IsWrapped=true)]
    public partial class GetOldestStateEntitySetIdResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://Sitel.TedisNet.System1.SystemDataService", Order=0)]
        public bool GetOldestStateEntitySetIdResult;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://Sitel.TedisNet.System1.SystemDataService", Order=1)]
        public System.Nullable<int> tagValueChangeId;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://Sitel.TedisNet.System1.SystemDataService", Order=2)]
        public System.Nullable<int> eventId;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://Sitel.TedisNet.System1.SystemDataService", Order=3)]
        public System.Nullable<int> commandExecutionId;
        
        public GetOldestStateEntitySetIdResponse() {
        }
        
        public GetOldestStateEntitySetIdResponse(bool GetOldestStateEntitySetIdResult, System.Nullable<int> tagValueChangeId, System.Nullable<int> eventId, System.Nullable<int> commandExecutionId) {
            this.GetOldestStateEntitySetIdResult = GetOldestStateEntitySetIdResult;
            this.tagValueChangeId = tagValueChangeId;
            this.eventId = eventId;
            this.commandExecutionId = commandExecutionId;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IStateServiceChannel : ConsoleApp8.ServiceReference1.IStateService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class StateServiceClient : System.ServiceModel.ClientBase<ConsoleApp8.ServiceReference1.IStateService>, ConsoleApp8.ServiceReference1.IStateService {
        
        public StateServiceClient() {
        }
        
        public StateServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public StateServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StateServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StateServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public ConsoleApp8.ServiceReference1.GetStateEntitySetResponse GetStateEntitySet(ConsoleApp8.ServiceReference1.GetStateEntitySetRequest request) {
            return base.Channel.GetStateEntitySet(request);
        }
        
        public ConsoleApp8.ServiceReference1.GetTagValuesResponse GetTagValues(ConsoleApp8.ServiceReference1.GetTagValuesRequest request) {
            return base.Channel.GetTagValues(request);
        }
        
        public ConsoleApp8.ServiceReference1.GetTagValuesBetweenDatesResponse GetTagValuesBetweenDates(ConsoleApp8.ServiceReference1.GetTagValuesBetweenDatesRequest request) {
            return base.Channel.GetTagValuesBetweenDates(request);
        }
        
        public ConsoleApp8.ServiceReference1.GetOldestStateEntitySetIdResponse GetOldestStateEntitySetId(ConsoleApp8.ServiceReference1.GetOldestStateEntitySetIdRequest request) {
            return base.Channel.GetOldestStateEntitySetId(request);
        }
    }
}