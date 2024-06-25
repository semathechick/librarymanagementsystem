using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class LoanTransactionConfiguration : IEntityTypeConfiguration<LoanTransaction>
{
    public void Configure(EntityTypeBuilder<LoanTransaction> builder)
    {
        builder.ToTable("LoanTransactions").HasKey(lt => lt.Id);

        builder.Property(lt => lt.Id).HasColumnName("Id").IsRequired();
        builder.Property(lt => lt.MemberId).HasColumnName("MemberId");
        builder.Property(lt => lt.BookId).HasColumnName("BookId");
        builder.Property(lt => lt.ReturnStatus).HasColumnName("ReturnStatus");
        builder.Property(lt => lt.ReturnTime).HasColumnName("ReturnTime");
        builder.Property(lt => lt.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(lt => lt.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(lt => lt.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(lt => !lt.DeletedDate.HasValue);
    }
}