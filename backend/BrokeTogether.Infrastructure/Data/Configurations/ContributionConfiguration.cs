using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrokeTogether.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrokeTogether.Infrastructure.Data.Configurations
{
    public class ContributionConfiguration : IEntityTypeConfiguration<Contribution>
    {
        public void Configure(EntityTypeBuilder<Contribution> builder)
        {
            builder.HasKey(c => c.Id);
            // A Contribution has many Splits
            builder.HasMany(c => c.Splits)
                .WithOne(cs => cs.Contribution)
                .HasForeignKey(cs => cs.ContributionId)
                .OnDelete(DeleteBehavior.Cascade);

            // A Contribution has many PurchasedItems
            builder.HasMany(c => c.PurchasedItems)
                .WithOne(sli => sli.Contribution)
                .HasForeignKey(sli => sli.ContributionId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}