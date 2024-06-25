using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class CategoryBookConfiguration : IEntityTypeConfiguration<CategoryBook>
{
    public void Configure(EntityTypeBuilder<CategoryBook> builder)
    {
        builder.ToTable("CategoryBooks").HasKey(cb => cb.Id);

        builder.Property(cb => cb.Id).HasColumnName("Id").IsRequired();
        builder.Property(cb => cb.BookId).HasColumnName("BookId");
        builder.Property(cb => cb.CategoryId).HasColumnName("CategoryId");
        builder.Property(cb => cb.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(cb => cb.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(cb => cb.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(cb => !cb.DeletedDate.HasValue);
    }
}