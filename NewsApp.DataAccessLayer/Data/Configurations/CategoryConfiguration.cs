using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsApp.Domain.Models;

namespace NewsApp.DataAccessLayer.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {

            builder.Property(T => T.Title)
                .HasMaxLength(100);
            builder.HasIndex(n => n.Title)
                .IsUnique();
        }
    }
}
