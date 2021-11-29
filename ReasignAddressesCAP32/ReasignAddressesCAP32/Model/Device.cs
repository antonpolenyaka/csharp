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
    
    public partial class Device
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Device()
        {
            this.ParentDevices = new HashSet<Device>();
            this.Tags = new HashSet<Tag>();
            this.VirtualDeviceTags = new HashSet<Tag>();
            this.Ports = new HashSet<Port>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public Nullable<int> Address { get; set; }
        public Nullable<int> SlaveNumber { get; set; }
        public string Comments { get; set; }
        public int DeviceTypeId { get; set; }
        public Nullable<int> ParentDeviceId { get; set; }
        public Nullable<int> ConnectionStateTagId { get; set; }
        public Nullable<int> ConnectionPetStateTagId { get; set; }
        public Nullable<int> ExportCode { get; set; }
        public Nullable<int> ExtensionPort1Type { get; set; }
        public Nullable<int> ExtensionPort2Type { get; set; }
        public Nullable<int> ExtensionPort3Type { get; set; }
        public bool IsAnalogMonopolar { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Device> ParentDevices { get; set; }
        public virtual Device ParentDevice { get; set; }
        public virtual DeviceType DeviceType { get; set; }
        public virtual Tag ConnectionStateTag { get; set; }
        public virtual Tag ConnectionPetStateTag { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tag> Tags { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tag> VirtualDeviceTags { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Port> Ports { get; set; }
    }
}
