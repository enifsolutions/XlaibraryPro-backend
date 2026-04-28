using Microsoft.Extensions.DependencyInjection;
using XlibraryPro.Application.Common.Interfaces;
using XlibraryPro.Domain.Interfaces;
using XlibraryPro.Domain.Interfaces.IMasterData;
using XlibraryPro.Infrastructure.Configuration;
using XlibraryPro.Infrastructure.Persistence.Repositories;
using XlibraryPro.Infrastructure.Persistence.Repositories.MasterData;
using XlibraryPro.Infrastructure.Services;

namespace XlibraryPro.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Connection factory
        services.AddSingleton<IConnectionFactory, DbConnectionFactory>();

        // Book repositories
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IBookCopyRepository, BookCopyRepository>();

        // Bulk upload services
        services.AddScoped<IBookFileParser, BookFileParser>();
        services.AddScoped<IBulkBookLookupService, BulkBookLookupService>();

        // Master Data repositories
        services.AddScoped<ILanguageRepository, LanguageRepository>();
        services.AddScoped<IBookTypeRepository, BookTypeRepository>();
        services.AddScoped<IBookStatusRepository, BookStatusRepository>();
        services.AddScoped<IShelfTypeRepository, ShelfTypeRepository>();
        services.AddScoped<IDeweyClassRepository, DeweyClassRepository>();
        services.AddScoped<IPublisherRepository, PublisherRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<ILoanStatusRepository, LoanStatusRepository>();
        services.AddScoped<IMemberStatusRepository, MemberStatusRepository>();
        services.AddScoped<IStudentBatchRepository, StudentBatchRepository>();
        services.AddScoped<IColourCodeRepository, ColourCodeRepository>();

        return services;
    }
}