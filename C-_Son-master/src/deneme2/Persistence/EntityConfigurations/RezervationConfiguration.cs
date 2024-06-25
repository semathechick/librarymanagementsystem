using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class RezervationConfiguration : IEntityTypeConfiguration<Rezervation>
{
    public void Configure(EntityTypeBuilder<Rezervation> builder)
    {
        builder.ToTable("Rezervations").HasKey(r => r.Id);

        builder.Property(r => r.Id).HasColumnName("Id").IsRequired();
        builder.Property(r => r.Id).HasColumnName("Id");
        builder.Property(r => r.BookId).HasColumnName("BookId");
        builder.Property(r => r.MemberId).HasColumnName("MemberId");
        builder.Property(r => r.RezervationDate).HasColumnName("RezervationDate");
        builder.Property(r => r.ExpirationDate).HasColumnName("ExpirationDate");
        builder.Property(r => r.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(r => r.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(r => r.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(r => !r.DeletedDate.HasValue);
    }
}