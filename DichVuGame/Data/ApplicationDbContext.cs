﻿using System;
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
        public DbSet<Banner> Banners { get; set; }
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
        public DbSet<RentalHistory> RentalHistories { get; set; }
        public DbSet<Comment> Comments { get; set; }    
        public DbSet<Review> Reviews { get; set; }
        public DbSet<GameComment> GameComments { get; set; }
        public DbSet<GameReview> GameReviews { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<RentalDetail> RentalDetails { get; set; }
        public DbSet<Launcher> Launchers { get; set; }
        public DbSet<GameLauncher> GameLaunchers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<GameTag>().HasKey(key => new { key.GameID, key.TagID });
            modelBuilder.Entity<GameReview>().HasKey(key => new { key.GameID, key.ReviewID });
            modelBuilder.Entity<GameComment>().HasKey(key => new { key.GameID, key.CommentID });
            modelBuilder.Entity<OrderDetail>().HasKey(key => new { key.OrderID, key.CodeID });
            modelBuilder.Entity<RentalDetail>().HasKey(key => new { key.RentalHistoryID, key.GameAccountID });
            modelBuilder.Entity<GameLauncher>().HasKey(key => new { key.LauncherID, key.GameID });
        }
        public DbSet<DichVuGame.Models.New> News { get; set; }
        public DbSet<DichVuGame.Models.Discount> Discount { get; set; }
    }
}
