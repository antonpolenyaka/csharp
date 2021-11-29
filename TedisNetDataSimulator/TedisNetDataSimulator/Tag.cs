//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TedisNetDataSimulator
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tag
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tag()
        {
            this.AlertTags = new HashSet<AlertTag>();
            this.Commands = new HashSet<Command>();
            this.Controls = new HashSet<Control>();
            this.Devices = new HashSet<Device>();
            this.Devices1 = new HashSet<Device>();
            this.Elements = new HashSet<Element>();
            this.Operands = new HashSet<Operand>();
            this.Operations = new HashSet<Operation>();
            this.TagIntervalSummaries = new HashSet<TagIntervalSummary>();
            this.TagIntervalValues = new HashSet<TagIntervalValue>();
            this.TagPropertyValues = new HashSet<TagPropertyValue>();
            this.TagScales = new HashSet<TagScale>();
            this.TagValueChanges = new HashSet<TagValueChanx>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public Nullable<int> Address { get; set; }
        public Nullable<int> TelecontrolAddress { get; set; }
        public Nullable<int> TelecontrolIOAddress { get; set; }
        public Nullable<int> LocalAddress { get; set; }
        public Nullable<int> DeviceTerminal { get; set; }
        public Nullable<int> TagSetTerminal { get; set; }
        public string OpcItemId { get; set; }
        public string OpcItemIdSelection { get; set; }
        public Nullable<int> ExportCode { get; set; }
        public string Comments { get; set; }
        public int DeviceId { get; set; }
        public Nullable<int> TagSetId { get; set; }
        public int TagClassId { get; set; }
        public Nullable<int> ElementId { get; set; }
        public Nullable<double> ActivationTime { get; set; }
        public Nullable<int> SendPriorityId { get; set; }
        public Nullable<bool> IsCommonMode { get; set; }
        public Nullable<bool> IsMonopolar { get; set; }
        public Nullable<bool> IsInverted { get; set; }
        public Nullable<int> StoreInterval { get; set; }
        public Nullable<int> SourceValueCodecId { get; set; }
        public Nullable<int> SourceValueTypeId { get; set; }
        public Nullable<bool> IsWritten { get; set; }
        public Nullable<int> VirtualDeviceId { get; set; }
        public Nullable<int> VirtualAddress { get; set; }
        public Nullable<int> DeviceScale { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlertTag> AlertTags { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Command> Commands { get; set; }
        public virtual Command Command { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Control> Controls { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Device> Devices { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Device> Devices1 { get; set; }
        public virtual Device Device { get; set; }
        public virtual Device Device1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Element> Elements { get; set; }
        public virtual Element Element { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Operand> Operands { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Operation> Operations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TagIntervalSummary> TagIntervalSummaries { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TagIntervalValue> TagIntervalValues { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TagPropertyValue> TagPropertyValues { get; set; }
        public virtual TagSet TagSet { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TagScale> TagScales { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TagValueChanx> TagValueChanges { get; set; }
        public virtual TagValue TagValue { get; set; }
    }
}