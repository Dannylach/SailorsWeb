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
    
    public partial class RentingEquipment
    {
        public int rentId { get; set; }
        public Nullable<int> renterId { get; set; }
        public Nullable<int> equipmentId { get; set; }
        public Nullable<System.DateTime> rentTimeStart { get; set; }
        public Nullable<System.DateTime> rentTimeEnd { get; set; }
    
        public virtual SailingEquipment SailingEquipment { get; set; }
        public virtual Users Users { get; set; }
    }
}
