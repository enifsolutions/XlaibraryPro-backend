namespace XlibraryPro.Application.Common.Interfaces;

/// <summary>
/// Resolves (and auto-creates if missing) lookup IDs for bulk upload.
/// </summary>
public interface IBulkBookLookupService
{
    Task<long> GetOrCreateLanguageAsync(string name, CancellationToken ct = default);
    Task<long> GetOrCreateBookTypeAsync(string name, CancellationToken ct = default);
    Task<long> GetOrCreatePublisherAsync(string name, CancellationToken ct = default);
    Task<long> GetOrCreateDeweyClassAsync(string number, CancellationToken ct = default);
    Task<long> GetOrCreateGenreAsync(string name, CancellationToken ct = default);
    Task<long?> FindAuthorByNameAsync(string fullName, CancellationToken ct = default);
    Task<long> CreateAuthorAsync(string firstName, string? middleName, string lastName, CancellationToken ct = default);
}