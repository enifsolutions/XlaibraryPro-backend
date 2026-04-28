using Microsoft.Extensions.DependencyInjection;
using XlibraryPro.Application.Common.Interfaces;
using XlibraryPro.Domain.Interfaces;
using XlibraryPro.Infrastructure.Configuration;
using XlibraryPro.Infrastructure.Persistence.Repositories;
using XlibraryPro.Infrastructure.Services;

namespace XlibraryPro.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Connection factory
        services.AddSingleton<IConnectionFactory, DbConnectionFactory>();

        // Book repositories
        services.AddScoped<IBookRepository,       BookRepository>();
        services.AddScoped<IBookCopyRepository,   BookCopyRepository>();
        services.AddScoped<IAuthorRepository,     AuthorRepository>();
        services.AddScoped<IGenreRepository,      GenreRepository>();
        services.AddScoped<IPublisherRepository,  PublisherRepository>();
        services.AddScoped<IDeweyClassRepository, DeweyClassRepository>();
        services.AddScoped<ILanguageRepository,   LanguageRepository>();
        services.AddScoped<IBookStatusRepository, BookStatusRepository>();

        // Bulk upload services
        services.AddScoped<IBookFileParser,        BookFileParser>();
        services.AddScoped<IBulkBookLookupService, BulkBookLookupService>();

        return services;
    }
}