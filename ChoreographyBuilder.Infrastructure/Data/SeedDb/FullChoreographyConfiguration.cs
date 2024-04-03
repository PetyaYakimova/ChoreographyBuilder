using ChoreographyBuilder.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoreographyBuilder.Infrastructure.Data.SeedDb
{
    internal class FullChoreographyConfiguration : IEntityTypeConfiguration<FullChoreography>
    {
        public void Configure(EntityTypeBuilder<FullChoreography> builder)
        {
            var data = new SeedData();

            builder.HasData(new FullChoreography[] { data.FullChoreography });
        }
    }
}
