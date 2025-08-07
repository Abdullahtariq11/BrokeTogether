using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrokeTogether.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrokeTogether.Infrastructure.Data.Configurations
{
    public class HomeMemberConfiguration : IEntityTypeConfiguration<HomeMember>
    {
        public void Configure(EntityTypeBuilder<HomeMember> builder)
        {
            // Define a composite primary key. Note UserId is now a string.
        builder.HasKey(hm => new { hm.UserId, hm.HomeId });
        }
    }
}