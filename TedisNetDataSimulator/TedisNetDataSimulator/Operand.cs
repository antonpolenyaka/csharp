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
    
    public partial class Operand
    {
        public int Id { get; set; }
        public int OperationId { get; set; }
        public int TagId { get; set; }
        public string FormulaVariable { get; set; }
    
        public virtual Operation Operation { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
