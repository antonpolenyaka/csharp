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
    
    public partial class ChannelState
    {
        public int ChannelId { get; set; }
        public bool IsEnabled { get; set; }
        public int Priority { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<System.DateTime> UpdateTimestamp { get; set; }
    
        public virtual Channel Channel { get; set; }
    }
}
