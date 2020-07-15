using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IssueTracker.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace IssueTracker.DAL
{
    public class WitContext : DbContext
    {
        public WitContext()
            : base("WitContextConnnect")
        {
        }

        public DbSet<IssueModel> Issues { get; set; }
        public DbSet<ProjectModel> Projects { get; set; }
        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<ProjectModel>()
                .HasMany(p => p.Users).WithMany(u => u.Projects)
                .Map(t => t.MapLeftKey("ProjectID")
                    .MapRightKey("UserID")
                    .ToTable("ProjectModelUserModel"));
        }
    }
}