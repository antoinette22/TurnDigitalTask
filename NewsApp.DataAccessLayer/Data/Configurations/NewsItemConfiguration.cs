using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.DataAccessLayer.Data.Configurations
{
    public class NewsItemConfiguration : IEntityTypeConfiguration<NewsItem>
    {
        public void Configure(EntityTypeBuilder<NewsItem> builder)
        {
            builder.HasIndex(n => new { n.Title, n.CategoryId })
            .IsUnique();

            builder.Property(T => T.Title)
                .HasMaxLength(100);

        }
    }
}
