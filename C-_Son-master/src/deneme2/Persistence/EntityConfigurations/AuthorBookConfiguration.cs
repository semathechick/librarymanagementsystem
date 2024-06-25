using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class AuthorBookConfiguration : IEntityTypeConfiguration<AuthorBook>
{
    public void Configure(EntityTypeBuilder<AuthorBook> builder)
    {
        builder.ToTable("AuthorBooks").HasKey(ab => ab.Id);

        builder.Property(ab => ab.Id).HasColumnName("Id").IsRequired();
        builder.Property(ab => ab.AuthorId).HasColumnName("AuthorId");
        builder.Property(ab => ab.BookId).HasColumnName("BookId");
        builder.Property(ab => ab.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ab => ab.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ab => ab.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(ab => !ab.DeletedDate.HasValue);
    }
}