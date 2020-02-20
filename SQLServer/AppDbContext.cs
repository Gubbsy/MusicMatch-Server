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
        public DbSet<GenreDbo> Genres { get; set; }
        public DbSet<VenueDbo> Venues { get; set; }
        public DbSet<UserGenreDbo> UserGenre { get; set; }
        public DbSet<UserVenueDbo> UserVenue { get; set; }

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
            SetUpManyToManyRelationships(modelBuilder);
            SeedRoles(modelBuilder);
        }

        private void SetUpPrimarykeys(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserGenreDbo>()
            .HasKey(ug => new { ug.UserId, ug.GenreId });

            modelBuilder.Entity<UserVenueDbo>()
            .HasKey(ug => new { ug.UserId, ug.VenueId });

            modelBuilder.Entity<TestDbo>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<GenreDbo>()
                .HasKey(g => g.Id);

            modelBuilder.Entity<VenueDbo>()
                .HasKey(v => v.Id);
        }

        private void SetUpManyToManyRelationships(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserGenreDbo>()
            .HasOne(ug => ug.Genre)
            .WithMany(u => u.AssociatedUsers)
            .HasForeignKey(ug => ug.GenreId);

            modelBuilder.Entity<UserGenreDbo>()
            .HasOne(ug => ug.AssociatedUser)
            .WithMany(u => u.Genres)
            .HasForeignKey(ug => ug.UserId);

            modelBuilder.Entity<UserVenueDbo>()
           .HasOne(ug => ug.Venue)
           .WithMany(u => u.AssociatedUsers)
           .HasForeignKey(ug => ug.VenueId);

            modelBuilder.Entity<UserVenueDbo>()
            .HasOne(ug => ug.AssociatedUser)
            .WithMany(u => u.Venues)
            .HasForeignKey(ug => ug.UserId);
        }

        private void SetUpOneToManyRelationships(ModelBuilder modelBuilder)
        {
        }

        private void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                   new IdentityRole
                   {
                       Name = AccountRoles.Artist,
                       NormalizedName = AccountRoles.Artist.ToUpperInvariant()
                   },
                   new IdentityRole
                   {
                       Name = AccountRoles.EventsManager,
                       NormalizedName = AccountRoles.EventsManager.ToUpperInvariant()
                   });
        }
    }
}