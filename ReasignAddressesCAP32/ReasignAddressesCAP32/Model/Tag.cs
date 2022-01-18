//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReasignAddressesCAP32.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tag
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tag()
        {
            this.Devices = new HashSet<Device>();
            this.DevicesConnectionPetState = new HashSet<Device>();
            this.Operands = new HashSet<Operand>();
            this.Operations = new HashSet<Operation>();
            this.Elements = new HashSet<Element>();
            this.Controls = new HashSet<Control>();
            this.TagScales = new HashSet<TagScale>();
            this.Commands = new HashSet<Command>();
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
        public Nullable<int> SensorValueTypeId { get; set; }
        public Nullable<int> DeviceOffset { get; set; }
        public Nullable<int> SourceAsduCode { get; set; }
        public Nullable<double> DeviceScaleFactorDec { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Device> Devices { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Device> DevicesConnectionPetState { get; set; }
        public virtual Device Device { get; set; }
        public virtual Device DeviceVirtualDevice { get; set; }
        public virtual TagClass TagClass { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Operand> Operands { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Operation> Operations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Element> Elements { get; set; }
        public virtual Element Element { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Control> Controls { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TagScale> TagScales { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Command> Commands { get; set; }
        public virtual Command Command { get; set; }
    }
}
