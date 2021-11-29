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
    
    public partial class Control
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Control()
        {
            this.ControlPositions = new HashSet<ControlPosition>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Comments { get; set; }
        public int ControlTypeId { get; set; }
        public string Text { get; set; }
        public Nullable<int> DialogActionId { get; set; }
        public Nullable<int> ElementId { get; set; }
        public Nullable<int> TagId { get; set; }
        public string ForegroundColor { get; set; }
        public string BackgroundColor { get; set; }
        public Nullable<int> Width { get; set; }
        public Nullable<int> Height { get; set; }
        public Nullable<int> FontSize { get; set; }
        public int NetworkId { get; set; }
        public int RotationAngle { get; set; }
        public bool IsEnabled { get; set; }
        public Nullable<int> RegionId { get; set; }
        public Nullable<int> ImageId { get; set; }
        public Nullable<int> GoToNetworkId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ControlPosition> ControlPositions { get; set; }
        public virtual Element Element { get; set; }
        public virtual Network Network { get; set; }
        public virtual Network Network1 { get; set; }
        public virtual Region Region { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
