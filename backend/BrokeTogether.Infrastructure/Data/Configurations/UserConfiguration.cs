using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrokeTogether.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrokeTogether.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // A User has many HomeMemberships
        builder.HasMany(u => u.HomeMemeberships)
            .WithOne(hm => hm.User)
            .HasForeignKey(hm => hm.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // A User has paid for many Contributions
        builder.HasMany(u => u.ContributionsPaid)
            .WithOne(c => c.PaidBy)
            .HasForeignKey(c => c.PaidById)
            .OnDelete(DeleteBehavior.Restrict);

        // A User has many OwedSplits
        builder.HasMany(u => u.OwedSplits)
            .WithOne(cs => cs.User)
            .HasForeignKey(cs => cs.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}