﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataBaseAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class yndlingsfilmDBEntities : DbContext
    {
        public yndlingsfilmDBEntities()
            : base("name=yndlingsfilmDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Highscore> Highscore { get; set; }
        public virtual DbSet<Movies> Movies { get; set; }
        public virtual DbSet<Rated> Rated { get; set; }
        public virtual DbSet<Relationship> Relationship { get; set; }
        public virtual DbSet<Reviews> Reviews { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}
