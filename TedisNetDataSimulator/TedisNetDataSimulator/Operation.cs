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
    
    public partial class Operation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Operation()
        {
            this.Operands = new HashSet<Operand>();
        }
    
        public int Id { get; set; }
        public int ResultTagId { get; set; }
        public string Name { get; set; }
        public int OperationTypeId { get; set; }
        public string Formula { get; set; }
        public Nullable<double> ValueOn { get; set; }
        public Nullable<double> ValueOff { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Operand> Operands { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
