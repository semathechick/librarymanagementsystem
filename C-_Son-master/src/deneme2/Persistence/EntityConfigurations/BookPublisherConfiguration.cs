using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class BookPublisherConfiguration : IEntityTypeConfiguration<BookPublisher>
{
    public void Configure(EntityTypeBuilder<BookPublisher> builder)
    {
        builder.ToTable("BookPublishers").HasKey(bp => bp.Id);

        builder.Property(bp => bp.Id).HasColumnName("Id").IsRequired();
        builder.Property(bp => bp.BookId).HasColumnName("BookId");
        builder.Property(bp => bp.PublisherId).HasColumnName("PublisherId");
        builder.Property(bp => bp.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(bp => bp.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(bp => bp.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(bp => !bp.DeletedDate.HasValue);
    }
}