//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAceess.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AttendanceEntities : DbContext
    {
        public AttendanceEntities()
            : base("name=AttendanceEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AttendanceLog> AttendanceLogs { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Employe> Employes { get; set; }
        public virtual DbSet<FingerPrintDevice> FingerPrintDevices { get; set; }
    }
}
