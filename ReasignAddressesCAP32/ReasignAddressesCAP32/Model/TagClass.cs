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
    
    public partial class TagClass
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TagClass()
        {
            this.Tags = new HashSet<Tag>();
            this.TagClasses1 = new HashSet<TagClass>();
            this.TagClasses11 = new HashSet<TagClass>();
            this.ElementTypes = new HashSet<ElementType>();
            this.CommandClasses = new HashSet<CommandClass>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string Comments { get; set; }
        public Nullable<int> ValueTypeId { get; set; }
        public Nullable<int> ParentTagClassId { get; set; }
        public Nullable<int> TargetStateTagClassId { get; set; }
        public Nullable<int> EventTypeId { get; set; }
        public string Units { get; set; }
        public string Format { get; set; }
        public Nullable<double> ActivationTime { get; set; }
        public Nullable<int> SendPriorityId { get; set; }
        public Nullable<bool> IsCommonMode { get; set; }
        public Nullable<bool> IsMonopolar { get; set; }
        public Nullable<bool> IsInverted { get; set; }
        public Nullable<int> ExportCode { get; set; }
        public Nullable<int> StoreInterval { get; set; }
        public Nullable<int> SourceValueCodecId { get; set; }
        public Nullable<int> SourceValueTypeId { get; set; }
        public Nullable<int> SignalActivationModeId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tag> Tags { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TagClass> TagClasses1 { get; set; }
        public virtual TagClass TagClass1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TagClass> TagClasses11 { get; set; }
        public virtual TagClass TagClass2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ElementType> ElementTypes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommandClass> CommandClasses { get; set; }
        public virtual CommandClass CommandClass { get; set; }
    }
}
