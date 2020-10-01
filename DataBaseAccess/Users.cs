//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataBaseAccess
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    
    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            this.Relationship = new HashSet<Relationship>();
        }
    
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Nullable<int> ReviewId { get; set; }
        public Nullable<int> RatedId { get; set; }
        public Nullable<int> MovieId { get; set; }
        [JsonIgnore]
        public virtual Movies Movies { get; set; }
        [JsonIgnore]
        public virtual Rated Rated { get; set; }
        [JsonIgnore]
        public virtual Reviews Reviews { get; set; }
        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Relationship> Relationship { get; set; }
    }
}
