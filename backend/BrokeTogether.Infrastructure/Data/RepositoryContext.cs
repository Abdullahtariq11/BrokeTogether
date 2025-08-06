using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BrokeTogether.Core.Entities;
using BrokeTogether.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BrokeTogether.Infrastructure.Data
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options) { }

        // The Users DbSet is now provided by IdentityDbContext.
        // We only need to add our custom entities.
        public DbSet<Home> Homes { get; set; } = null!;
        public DbSet<HomeMember> HomeMembers { get; set; } = null!;
        public DbSet<Contribution> Contributions { get; set; } = null!;
        public DbSet<ContributionSplit> ContributionSplits { get; set; } = null!;
        public DbSet<ShoppingListItem> ShoppingListItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); //Crucial for identity

            // This one line still works perfectly to apply all your custom configurations.

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}