using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrokeTogether.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrokeTogether.Infrastructure.Data.Configurations
{
    public class HomeConfiguration : IEntityTypeConfiguration<Home>
    {
        public void Configure(EntityTypeBuilder<Home> builder)
        {
            builder.HasKey(h => h.Id);
            builder.HasIndex(h => h.InviteCode).IsUnique();

            builder.HasMany(h => h.Members)
                .WithOne(hm => hm.Home)
                .HasForeignKey(hm => hm.HomeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(h => h.Contributions)
                .WithOne(c => c.Home)
                .HasForeignKey(c => c.HomeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(h => h.ShoppingListItems)
                .WithOne(sli => sli.Home)
                .HasForeignKey(sli => sli.HomeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}