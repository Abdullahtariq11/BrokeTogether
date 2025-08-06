using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrokeTogether.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrokeTogether.Infrastructure.Data.Configurations
{
    public class ContributionSplitConfiguration : IEntityTypeConfiguration<ContributionSplit>
    {
        public void Configure(EntityTypeBuilder<ContributionSplit> builder)
        {
            // Define a composite primary key
            builder.HasKey(cs => new { cs.ContributionId, cs.UserId });

            builder.Property(cs => cs.AmountOwed).HasColumnType("decimal(18,2)");
        }
    }
}