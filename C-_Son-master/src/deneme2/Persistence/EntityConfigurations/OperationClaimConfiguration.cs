using Application.Features.Auth.Constants;
using Application.Features.OperationClaims.Constants;
using Application.Features.UserOperationClaims.Constants;
using Application.Features.Users.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NArchitecture.Core.Security.Constants;
using Application.Features.Books.Constants;
using Application.Features.Categories.Constants;
using Application.Features.Authors.Constants;
using Application.Features.Rezervations.Constants;

using Application.Features.AuthorBooks.Constants;
using Application.Features.CategoryBooks.Constants;
using Application.Features.Publishers.Constants;
using Application.Features.BookPublishers.Constants;
using Application.Features.Members.Constants;
using Application.Features.LoanTransactions.Constants;


namespace Persistence.EntityConfigurations;

public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
{
    public void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
        builder.ToTable("OperationClaims").HasKey(oc => oc.Id);

        builder.Property(oc => oc.Id).HasColumnName("Id").IsRequired();
        builder.Property(oc => oc.Name).HasColumnName("Name").IsRequired();
        builder.Property(oc => oc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(oc => oc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(oc => oc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(oc => !oc.DeletedDate.HasValue);

        builder.HasData(_seeds);

        builder.HasBaseType((string)null!);
    }

    public static int AdminId => 1;
    private IEnumerable<OperationClaim> _seeds
    {
        get
        {
            yield return new() { Id = AdminId, Name = GeneralOperationClaims.Admin };

            IEnumerable<OperationClaim> featureOperationClaims = getFeatureOperationClaims(AdminId);
            foreach (OperationClaim claim in featureOperationClaims)
                yield return claim;
        }
    }

#pragma warning disable S1854 // Unused assignments should be removed
    private IEnumerable<OperationClaim> getFeatureOperationClaims(int initialId)
    {
        int lastId = initialId;
        List<OperationClaim> featureOperationClaims = new();

        #region Auth
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = AuthOperationClaims.Admin },
                new() { Id = ++lastId, Name = AuthOperationClaims.Read },
                new() { Id = ++lastId, Name = AuthOperationClaims.Write },
                new() { Id = ++lastId, Name = AuthOperationClaims.RevokeToken },
            ]
        );
        #endregion

        #region OperationClaims
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Admin },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Read },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Write },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Create },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Update },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Delete },
            ]
        );
        #endregion

        #region UserOperationClaims
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Admin },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Read },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Write },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Create },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Update },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Delete },
            ]
        );
        #endregion

        #region Users
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = UsersOperationClaims.Admin },
                new() { Id = ++lastId, Name = UsersOperationClaims.Read },
                new() { Id = ++lastId, Name = UsersOperationClaims.Write },
                new() { Id = ++lastId, Name = UsersOperationClaims.Create },
                new() { Id = ++lastId, Name = UsersOperationClaims.Update },
                new() { Id = ++lastId, Name = UsersOperationClaims.Delete },
            ]
        );
        #endregion

        
        #region Books
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = BooksOperationClaims.Admin },
                new() { Id = ++lastId, Name = BooksOperationClaims.Read },
                new() { Id = ++lastId, Name = BooksOperationClaims.Write },
                new() { Id = ++lastId, Name = BooksOperationClaims.Create },
                new() { Id = ++lastId, Name = BooksOperationClaims.Update },
                new() { Id = ++lastId, Name = BooksOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Categories
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Admin },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Read },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Write },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Create },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Update },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Books
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = BooksOperationClaims.Admin },
                new() { Id = ++lastId, Name = BooksOperationClaims.Read },
                new() { Id = ++lastId, Name = BooksOperationClaims.Write },
                new() { Id = ++lastId, Name = BooksOperationClaims.Create },
                new() { Id = ++lastId, Name = BooksOperationClaims.Update },
                new() { Id = ++lastId, Name = BooksOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Categories
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Admin },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Read },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Write },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Create },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Update },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        
        
        
        #region AuthorBooks
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = AuthorBooksOperationClaims.Admin },
                new() { Id = ++lastId, Name = AuthorBooksOperationClaims.Read },
                new() { Id = ++lastId, Name = AuthorBooksOperationClaims.Write },
                new() { Id = ++lastId, Name = AuthorBooksOperationClaims.Create },
                new() { Id = ++lastId, Name = AuthorBooksOperationClaims.Update },
                new() { Id = ++lastId, Name = AuthorBooksOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region CategoryBooks
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = CategoryBooksOperationClaims.Admin },
                new() { Id = ++lastId, Name = CategoryBooksOperationClaims.Read },
                new() { Id = ++lastId, Name = CategoryBooksOperationClaims.Write },
                new() { Id = ++lastId, Name = CategoryBooksOperationClaims.Create },
                new() { Id = ++lastId, Name = CategoryBooksOperationClaims.Update },
                new() { Id = ++lastId, Name = CategoryBooksOperationClaims.Delete },
            ]
        );
        #endregion
        
      
        
        #region Books
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = BooksOperationClaims.Admin },
                new() { Id = ++lastId, Name = BooksOperationClaims.Read },
                new() { Id = ++lastId, Name = BooksOperationClaims.Write },
                new() { Id = ++lastId, Name = BooksOperationClaims.Create },
                new() { Id = ++lastId, Name = BooksOperationClaims.Update },
                new() { Id = ++lastId, Name = BooksOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region CategoryBooks
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = CategoryBooksOperationClaims.Admin },
                new() { Id = ++lastId, Name = CategoryBooksOperationClaims.Read },
                new() { Id = ++lastId, Name = CategoryBooksOperationClaims.Write },
                new() { Id = ++lastId, Name = CategoryBooksOperationClaims.Create },
                new() { Id = ++lastId, Name = CategoryBooksOperationClaims.Update },
                new() { Id = ++lastId, Name = CategoryBooksOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Categories
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Admin },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Read },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Write },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Create },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Update },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Delete },
            ]
        );
        #endregion
        
        
       
        
        
        #region Books
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = BooksOperationClaims.Admin },
                new() { Id = ++lastId, Name = BooksOperationClaims.Read },
                new() { Id = ++lastId, Name = BooksOperationClaims.Write },
                new() { Id = ++lastId, Name = BooksOperationClaims.Create },
                new() { Id = ++lastId, Name = BooksOperationClaims.Update },
                new() { Id = ++lastId, Name = BooksOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        
        
        #region Publishers
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = PublishersOperationClaims.Admin },
                new() { Id = ++lastId, Name = PublishersOperationClaims.Read },
                new() { Id = ++lastId, Name = PublishersOperationClaims.Write },
                new() { Id = ++lastId, Name = PublishersOperationClaims.Create },
                new() { Id = ++lastId, Name = PublishersOperationClaims.Update },
                new() { Id = ++lastId, Name = PublishersOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Books
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = BooksOperationClaims.Admin },
                new() { Id = ++lastId, Name = BooksOperationClaims.Read },
                new() { Id = ++lastId, Name = BooksOperationClaims.Write },
                new() { Id = ++lastId, Name = BooksOperationClaims.Create },
                new() { Id = ++lastId, Name = BooksOperationClaims.Update },
                new() { Id = ++lastId, Name = BooksOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Publishers
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = PublishersOperationClaims.Admin },
                new() { Id = ++lastId, Name = PublishersOperationClaims.Read },
                new() { Id = ++lastId, Name = PublishersOperationClaims.Write },
                new() { Id = ++lastId, Name = PublishersOperationClaims.Create },
                new() { Id = ++lastId, Name = PublishersOperationClaims.Update },
                new() { Id = ++lastId, Name = PublishersOperationClaims.Delete },
            ]
        );
        #endregion
        
     
        
        
        #region Books
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = BooksOperationClaims.Admin },
                new() { Id = ++lastId, Name = BooksOperationClaims.Read },
                new() { Id = ++lastId, Name = BooksOperationClaims.Write },
                new() { Id = ++lastId, Name = BooksOperationClaims.Create },
                new() { Id = ++lastId, Name = BooksOperationClaims.Update },
                new() { Id = ++lastId, Name = BooksOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Publishers
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = PublishersOperationClaims.Admin },
                new() { Id = ++lastId, Name = PublishersOperationClaims.Read },
                new() { Id = ++lastId, Name = PublishersOperationClaims.Write },
                new() { Id = ++lastId, Name = PublishersOperationClaims.Create },
                new() { Id = ++lastId, Name = PublishersOperationClaims.Update },
                new() { Id = ++lastId, Name = PublishersOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Books
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = BooksOperationClaims.Admin },
                new() { Id = ++lastId, Name = BooksOperationClaims.Read },
                new() { Id = ++lastId, Name = BooksOperationClaims.Write },
                new() { Id = ++lastId, Name = BooksOperationClaims.Create },
                new() { Id = ++lastId, Name = BooksOperationClaims.Update },
                new() { Id = ++lastId, Name = BooksOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region BookPublishers
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = BookPublishersOperationClaims.Admin },
                new() { Id = ++lastId, Name = BookPublishersOperationClaims.Read },
                new() { Id = ++lastId, Name = BookPublishersOperationClaims.Write },
                new() { Id = ++lastId, Name = BookPublishersOperationClaims.Create },
                new() { Id = ++lastId, Name = BookPublishersOperationClaims.Update },
                new() { Id = ++lastId, Name = BookPublishersOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Publishers
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = PublishersOperationClaims.Admin },
                new() { Id = ++lastId, Name = PublishersOperationClaims.Read },
                new() { Id = ++lastId, Name = PublishersOperationClaims.Write },
                new() { Id = ++lastId, Name = PublishersOperationClaims.Create },
                new() { Id = ++lastId, Name = PublishersOperationClaims.Update },
                new() { Id = ++lastId, Name = PublishersOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Members
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = MembersOperationClaims.Admin },
                new() { Id = ++lastId, Name = MembersOperationClaims.Read },
                new() { Id = ++lastId, Name = MembersOperationClaims.Write },
                new() { Id = ++lastId, Name = MembersOperationClaims.Create },
                new() { Id = ++lastId, Name = MembersOperationClaims.Update },
                new() { Id = ++lastId, Name = MembersOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region LoanTransactions
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = LoanTransactionsOperationClaims.Admin },
                new() { Id = ++lastId, Name = LoanTransactionsOperationClaims.Read },
                new() { Id = ++lastId, Name = LoanTransactionsOperationClaims.Write },
                new() { Id = ++lastId, Name = LoanTransactionsOperationClaims.Create },
                new() { Id = ++lastId, Name = LoanTransactionsOperationClaims.Update },
                new() { Id = ++lastId, Name = LoanTransactionsOperationClaims.Delete },
            ]
        );
        #endregion
        
     
     #region Authors
     featureOperationClaims.AddRange(
         [
             new() { Id = ++lastId, Name = AuthorsOperationClaims.Admin },
             new() { Id = ++lastId, Name = AuthorsOperationClaims.Read },
             new() { Id = ++lastId, Name = AuthorsOperationClaims.Write },
             new() { Id = ++lastId, Name = AuthorsOperationClaims.Create },
             new() { Id = ++lastId, Name = AuthorsOperationClaims.Update },
             new() { Id = ++lastId, Name = AuthorsOperationClaims.Delete },
         ]
     );
     #endregion
     
     
     #region LoanTransactions
     featureOperationClaims.AddRange(
         [
             new() { Id = ++lastId, Name = LoanTransactionsOperationClaims.Admin },
             new() { Id = ++lastId, Name = LoanTransactionsOperationClaims.Read },
             new() { Id = ++lastId, Name = LoanTransactionsOperationClaims.Write },
             new() { Id = ++lastId, Name = LoanTransactionsOperationClaims.Create },
             new() { Id = ++lastId, Name = LoanTransactionsOperationClaims.Update },
             new() { Id = ++lastId, Name = LoanTransactionsOperationClaims.Delete },
         ]
     );
     #endregion
     
     
     #region Rezervations
     featureOperationClaims.AddRange(
         [
             new() { Id = ++lastId, Name = RezervationsOperationClaims.Admin },
             new() { Id = ++lastId, Name = RezervationsOperationClaims.Read },
             new() { Id = ++lastId, Name = RezervationsOperationClaims.Write },
             new() { Id = ++lastId, Name = RezervationsOperationClaims.Create },
             new() { Id = ++lastId, Name = RezervationsOperationClaims.Update },
             new() { Id = ++lastId, Name = RezervationsOperationClaims.Delete },
         ]
     );
     #endregion
     
        return featureOperationClaims;
    }
#pragma warning restore S1854 // Unused assignments should be removed
}
