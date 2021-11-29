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
    
    public partial class TagValue
    {
        public int TagId { get; set; }
        public Nullable<bool> ValueBool { get; set; }
        public Nullable<int> ValueInt { get; set; }
        public Nullable<double> ValueFloat { get; set; }
        public string ValueStr { get; set; }
        public Nullable<int> ValueEnumId { get; set; }
        public Nullable<System.DateTime> SourceTimestamp { get; set; }
        public Nullable<System.DateTime> UpdateTimestamp { get; set; }
        public Nullable<int> QualityId { get; set; }
        public Nullable<int> QualityDetailId { get; set; }
        public Nullable<int> QualitySourceId { get; set; }
        public bool IsTagValueChangeHidden { get; set; }
    
        public virtual Tag Tag { get; set; }
    }
}