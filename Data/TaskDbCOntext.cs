using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using taskManager_API.Configurations;
using taskManager_API.models;

namespace taskManager_API.Data
{
    public class TaskDbContext : IdentityDbContext<AppUser>
    {
        public TaskDbContext(DbContextOptions dbContextOptions)
            :base(dbContextOptions)
        {
            
        }
        
        public DbSet<Tasks> tasks {get; set;}
        public DbSet<Category> categories {get; set;}
        public DbSet<Tags> tags {get; set;}
        public DbSet<TaskTag> taskTags {get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                }

            };

            builder.Entity<IdentityRole>().HasData(roles);
            builder.ApplyConfiguration(new TaskConfig());
            builder.ApplyConfiguration(new TaskTagConfig());
        }
    }
}