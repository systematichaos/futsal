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
    
    public partial class UserHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Application { get; set; }
        public string MachineName { get; set; }
        public string Properties { get; set; }
        public string Operation { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public string Exception { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
