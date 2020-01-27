using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SQLServer.Models;
using System;

namespace SQLServer
{
    public class AppDbContext : IdentityDbContext<ApplicationUserDbo>
    {
        //  Properties
        //  ==========

        public DbSet<TestDbo> Testdbos { get; set; }

        //  Constructors
        //  ============

        public AppDbContext(DbContextOptions<AppDbContext> contextOptions) : base(contextOptions)
        {

        }

        //  Methods
        //  =======

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder = modelBuilder ?? throw new ArgumentNullException(nameof(modelBuilder));

            SetUpPrimarykeys(modelBuilder);
            SetUpOneToManyRelationships(modelBuilder);
            SeedRoles(modelBuilder);
        }

        private void SetUpPrimarykeys(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestDbo>()
                .HasKey(t => t.Id);
        }

        private void SetUpOneToManyRelationships(ModelBuilder modelBuilder)
        {
        }

        private void SeedRoles(ModelBuilder modelBuilder)
        {
           
        }
    }
}