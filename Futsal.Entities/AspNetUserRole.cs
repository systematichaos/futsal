//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Futsal.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class AspNetUserRole
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> CreatedByUser { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public Nullable<int> LastUpdatedByUser { get; set; }
    
        public virtual AspNetRole AspNetRole { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
    }
}