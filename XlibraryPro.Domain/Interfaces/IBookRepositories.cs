using XlibraryPro.Domain.Entities;

namespace XlibraryPro.Domain.Interfaces;

// ── Book ──────────────────────────────────────────────────────────────────────

public interface IBookRepository
{
    Task<Book?>             GetByIdAsync(long id, CancellationToken ct = default);
    Task<Book?>             GetByIdWithDetailsAsync(long id, CancellationToken ct = default);
    Task<Book?>             GetByIsbnAsync(string isbn, CancellationToken ct = default);
    Task<IEnumerable<Book>> GetAllAsync(CancellationToken ct = default);
    Task<IEnumerable<Book>> SearchAsync(string searchTerm, CancellationToken ct = default);
    Task<IEnumerable<Book>> GetByAuthorAsync(long authorId, CancellationToken ct = default);
    Task<IEnumerable<Book>> GetByGenreAsync(long genreId, CancellationToken ct = default);
    Task<IEnumerable<Book>> GetByPublisherAsync(long publisherId, CancellationToken ct = default);
    Task<IEnumerable<Book>> GetByDeweyClassAsync(long deweyId, CancellationToken ct = default);
    Task                    AddAsync(Book book, CancellationToken ct = default);
    Task                    UpdateAsync(Book book, CancellationToken ct = default);
    Task                    DeleteAsync(long id, CancellationToken ct = default);
    Task<bool>              ExistsAsync(long id, CancellationToken ct = default);
    Task<bool>              IsbnExistsAsync(string isbn, long? excludeBookId = null, CancellationToken ct = default);
}

// ── BookCopy ──────────────────────────────────────────────────────────────────

public interface IBookCopyRepository
{
    Task<BookCopy?>             GetByIdAsync(long id, CancellationToken ct = default);
    Task<BookCopy?>             GetByBarcodeAsync(string barcode, CancellationToken ct = default);
    Task<IEnumerable<BookCopy>> GetByBookIdAsync(long bookId, CancellationToken ct = default);
    Task<IEnumerable<BookCopy>> GetAvailableCopiesAsync(long bookId, CancellationToken ct = default);
    Task                        AddAsync(BookCopy copy, CancellationToken ct = default);
    Task                        UpdateAsync(BookCopy copy, CancellationToken ct = default);
    Task                        DeleteAsync(long id, CancellationToken ct = default);
    Task<bool>                  BarcodeExistsAsync(string barcode, long? excludeCopyId = null, CancellationToken ct = default);
}

// ── Lookups ───────────────────────────────────────────────────────────────────

public interface IAuthorRepository
{
    Task<Author?>             GetByIdAsync(long id, CancellationToken ct = default);
    Task<IEnumerable<Author>> GetAllAsync(CancellationToken ct = default);
    Task<IEnumerable<Author>> SearchByNameAsync(string name, CancellationToken ct = default);
    Task                      AddAsync(Author author, CancellationToken ct = default);
    Task                      UpdateAsync(Author author, CancellationToken ct = default);
    Task                      DeleteAsync(long id, CancellationToken ct = default);
}

public interface IGenreRepository
{
    Task<Genre?>             GetByIdAsync(long id, CancellationToken ct = default);
    Task<IEnumerable<Genre>> GetAllAsync(CancellationToken ct = default);
    Task                     AddAsync(Genre genre, CancellationToken ct = default);
    Task                     UpdateAsync(Genre genre, CancellationToken ct = default);
    Task                     DeleteAsync(long id, CancellationToken ct = default);
}

public interface IPublisherRepository
{
    Task<Publisher?>             GetByIdAsync(long id, CancellationToken ct = default);
    Task<IEnumerable<Publisher>> GetAllAsync(CancellationToken ct = default);
    Task<IEnumerable<Publisher>> SearchByNameAsync(string name, CancellationToken ct = default);
    Task                         AddAsync(Publisher publisher, CancellationToken ct = default);
    Task                         UpdateAsync(Publisher publisher, CancellationToken ct = default);
}

public interface IDeweyClassRepository
{
    Task<DeweyClass?>             GetByIdAsync(long id, CancellationToken ct = default);
    Task<IEnumerable<DeweyClass>> GetAllAsync(CancellationToken ct = default);
    Task<IEnumerable<DeweyClass>> SearchAsync(string term, CancellationToken ct = default);
    Task                          AddAsync(DeweyClass deweyClass, CancellationToken ct = default);
}

public interface ILanguageRepository
{
    Task<Language?>             GetByIdAsync(long id, CancellationToken ct = default);
    Task<IEnumerable<Language>> GetAllAsync(CancellationToken ct = default);
    Task                        AddAsync(Language language, CancellationToken ct = default);
}

public interface IBookStatusRepository
{
    Task<BookStatus?>             GetByIdAsync(long id, CancellationToken ct = default);
    Task<IEnumerable<BookStatus>> GetAllAsync(CancellationToken ct = default);
    Task<BookStatus?>             GetByNameAsync(string name, CancellationToken ct = default);
}