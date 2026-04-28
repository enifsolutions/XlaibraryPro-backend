using XlibraryPro.Domain.Entities;

namespace XlibraryPro.Domain.Interfaces;

// ── Book ─────────────────────────────────────────────────────────────────────

public interface IBookRepository
{
    Task<Book?> GetByIdAsync(long id, CancellationToken ct = default);
    Task<Book?> GetByIdWithDetailsAsync(long id, CancellationToken ct = default);
    Task<Book?> GetByIsbnAsync(string isbn, CancellationToken ct = default);
    Task<IEnumerable<Book>> GetAllAsync(CancellationToken ct = default);
    Task<IEnumerable<Book>> SearchAsync(string searchTerm, CancellationToken ct = default);
    Task<IEnumerable<Book>> GetByAuthorAsync(long authorId, CancellationToken ct = default);
    Task<IEnumerable<Book>> GetByGenreAsync(long genreId, CancellationToken ct = default);
    Task<IEnumerable<Book>> GetByPublisherAsync(long publisherId, CancellationToken ct = default);
    Task<IEnumerable<Book>> GetByDeweyClassAsync(long deweyId, CancellationToken ct = default);
    Task AddAsync(Book book, CancellationToken ct = default);
    Task UpdateAsync(Book book, CancellationToken ct = default);
    Task DeleteAsync(long id, CancellationToken ct = default);
    Task<bool> ExistsAsync(long id, CancellationToken ct = default);
    Task<bool> IsbnExistsAsync(string isbn, long? excludeBookId = null, CancellationToken ct = default);
}

// ── BookCopy ──────────────────────────────────────────────────────────────────

public interface IBookCopyRepository
{
    Task<BookCopy?> GetByIdAsync(long id, CancellationToken ct = default);
    Task<BookCopy?> GetByBarcodeAsync(string barcode, CancellationToken ct = default);
    Task<IEnumerable<BookCopy>> GetByBookIdAsync(long bookId, CancellationToken ct = default);
    Task<IEnumerable<BookCopy>> GetAvailableCopiesAsync(long bookId, CancellationToken ct = default);
    Task AddAsync(BookCopy copy, CancellationToken ct = default);
    Task UpdateAsync(BookCopy copy, CancellationToken ct = default);
    Task DeleteAsync(long id, CancellationToken ct = default);
    Task<bool> BarcodeExistsAsync(string barcode, long? excludeCopyId = null, CancellationToken ct = default);
}