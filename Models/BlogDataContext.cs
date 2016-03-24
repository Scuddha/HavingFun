﻿using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaveFun.Models
{
    public class BlogDataContext : DbContext
    {
       public DbSet<Post> Posts { get; set; }

        public BlogDataContext()
        {
            Database.EnsureCreated();
        }

        public IQueryable<ArchivedPostsSummary> GetArchivedPosts()
        {
            return
              Posts
                    .GroupBy(x => new { x.Time.Year, x.Time.Month })
                    .Select(group => new ArchivedPostsSummary
                    {
                        Count = group.Count(),
                        Year = group.Key.Year,
                        Month = group.Key.Month,
                    });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var connectionString = @"Server=(LocalDb)\MSSQLLocalDb;Database=HaveFun";
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.UseSqlServerIdentityColumns();
        }
    }
}