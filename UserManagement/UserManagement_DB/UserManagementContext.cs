using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement_DB.Models;

namespace UserManagement_DB
{
    public class UserManagementContext : IdentityDbContext
    {
        public UserManagementContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasOne<UserProfile>(u => u.Profile)
                .WithOne(sa => sa.User)
                .HasForeignKey<UserProfile>(f => f.UserId); 

        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        

    }
}
