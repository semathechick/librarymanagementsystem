using Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NArchitecture.Core.Persistence.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BaseDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("BaseDb")));
        services.AddDbMigrationApplier(buildServices => buildServices.GetRequiredService<BaseDbContext>());

        services.AddScoped<IEmailAuthenticatorRepository, EmailAuthenticatorRepository>();
        services.AddScoped<IOperationClaimRepository, OperationClaimRepository>();
        services.AddScoped<IOtpAuthenticatorRepository, OtpAuthenticatorRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepository>();

        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        
        services.AddScoped<IAuthorBookRepository, AuthorBookRepository>();
        services.AddScoped<ICategoryBookRepository, CategoryBookRepository>();
        
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<ICategoryBookRepository, CategoryBookRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        
        services.AddScoped<IBookRepository, BookRepository>();


        services.AddScoped<IPublisherRepository, PublisherRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IPublisherRepository, PublisherRepository>();
        
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IPublisherRepository, PublisherRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IBookPublisherRepository, BookPublisherRepository>();
        services.AddScoped<IPublisherRepository, PublisherRepository>();
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<ILoanTransactionRepository, LoanTransactionRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<ILoanTransactionRepository, LoanTransactionRepository>();
        services.AddScoped<IRezervationRepository, RezervationRepository>();
        return services;
    }
}
