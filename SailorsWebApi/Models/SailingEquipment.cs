//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SailorsWebApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SailingEquipment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SailingEquipment()
        {
            this.RentingEquipment = new HashSet<RentingEquipment>();
        }
    
        public int equipmentId { get; set; }
        public string equipmentName { get; set; }
        public Nullable<int> equipmentType { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RentingEquipment> RentingEquipment { get; set; }
        public virtual EquipmentTypes EquipmentTypes { get; set; }
    }
}
