//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Arad.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reserve
    {
        public int Id { get; set; }
        public Nullable<int> SalonSanceId { get; set; }
        public Nullable<int> AccountId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Token { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> FinalPrice { get; set; }
    
        public virtual Account Account { get; set; }
        public virtual Salon_Sance Salon_Sance { get; set; }
    }
}