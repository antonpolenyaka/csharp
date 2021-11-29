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
    
    public partial class TagValueChanx
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TagValueChanx()
        {
            this.CommandExecutions = new HashSet<CommandExecution>();
            this.Events = new HashSet<Event>();
        }
    
        public int Id { get; set; }
        public int TagId { get; set; }
        public Nullable<bool> ValueBool { get; set; }
        public Nullable<int> ValueInt { get; set; }
        public Nullable<double> ValueFloat { get; set; }
        public string ValueStr { get; set; }
        public Nullable<int> ValueEnumId { get; set; }
        public string Comments { get; set; }
        public Nullable<System.DateTime> SourceTimestamp { get; set; }
        public Nullable<System.DateTime> UpdateTimestamp { get; set; }
        public Nullable<int> QualityId { get; set; }
        public Nullable<int> QualityDetailId { get; set; }
        public Nullable<int> QualitySourceId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommandExecution> CommandExecutions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event> Events { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
