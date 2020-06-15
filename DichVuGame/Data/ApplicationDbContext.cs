using System;
using System.Collections.Generic;
using System.Text;
using DichVuGame.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DichVuGame.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers  { get; set; }
        public DbSet<TopupHistory> TopupHistories { get; set; }
        public DbSet<Code> Codes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameAccount> GameAccounts { get; set; }
        public DbSet<GameTag> GameTags { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Studio> Studios { get; set; }
        public DbSet<SystemRequirement> SystemRequirements { get; set; }
        public DbSet<GameDemo> Demos { get; set; }
        public DbSet<RentalHistory> RentalHistories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<GameTag>().HasKey(key => new { key.GameID, key.TagID });
        }
    }
}
