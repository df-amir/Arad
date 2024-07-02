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
    
    public partial class Request
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Request()
        {
            this.Play = new HashSet<Play>();
        }
    
        public int Id { get; set; }
        public Nullable<int> CreatorAccountId { get; set; }
        public Nullable<int> HostTeamId { get; set; }
        public Nullable<int> GuestTeamId { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<int> RefereeAccountId { get; set; }
        public Nullable<int> Salon_Sance_Id { get; set; }
    
        public virtual Account Account { get; set; }
        public virtual Account Account1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Play> Play { get; set; }
        public virtual Salon_Sance Salon_Sance { get; set; }
        public virtual Team Team { get; set; }
        public virtual Team Team1 { get; set; }
    }
}