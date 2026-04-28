using Dapper;
using XlibraryPro.Domain.Entities;
using XlibraryPro.Domain.Interfaces;
using XlibraryPro.Infrastructure.Configuration;
using XlibraryPro.Infrastructure.Persistence.Models;

namespace XlibraryPro.Infrastructure.Persistence.Repositories;

public class BookCopyRepository(IConnectionFactory db) : IBookCopyRepository
{
    public async Task<BookCopy?> GetByIdAsync(long id, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var rows = await conn.QueryAsync<BookCopy>("SELECT * FROM fn_get_book_copies(@p_book_id)", new { p_book_id = 0 });
        return rows.FirstOrDefault(c => c.Id == id);
    }

    public async Task<BookCopy?> GetByBarcodeAsync(string barcode, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var rows = await conn.QueryAsync<BookCopy>("SELECT * FROM fn_get_copy_by_barcode(@p_barcode)", new { p_barcode = barcode });
        return rows.FirstOrDefault();
    }

    public async Task<IEnumerable<BookCopy>> GetByBookIdAsync(long bookId, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        return await conn.QueryAsync<BookCopy>("SELECT * FROM fn_get_book_copies(@p_book_id)", new { p_book_id = bookId });
    }

    public async Task<IEnumerable<BookCopy>> GetAvailableCopiesAsync(long bookId, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        return await conn.QueryAsync<BookCopy>("SELECT * FROM fn_get_available_copies(@p_book_id)", new { p_book_id = bookId });
    }

    public async Task<bool> BarcodeExistsAsync(string barcode, long? excludeCopyId = null, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var rows = await conn.QueryAsync<BookCopy>("SELECT * FROM fn_get_copy_by_barcode(@p_barcode)", new { p_barcode = barcode });
        return rows.Any(c => c.Id != excludeCopyId);
    }

    public async Task AddAsync(BookCopy copy, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var result = await conn.QuerySingleAsync<SpResult>(
            "CALL sp_action_book_copy(@v_out, @p_book_copies_id, @p_book_id, @p_barcode, " +
            "@p_book_status_id, @p_shelf_type_id, @p_shelf_location, " +
            "@p_acquisition_date, @p_condition, @p_notes, @p_action)",
            new
            {
                v_out              = 0,
                p_book_copies_id   = (long?)null,
                p_book_id          = copy.BookId,
                p_barcode          = copy.Barcode,
                p_book_status_id   = copy.BookStatusId,
                p_shelf_type_id    = copy.ShelfTypeId,
                p_shelf_location   = copy.ShelfLocation,
                p_acquisition_date = copy.AcquisitionDate,
                p_condition        = copy.Condition.ToString(),
                p_notes            = copy.Notes,
                p_action           = "ADD"
            });

        if (result.v_out == 99)
            throw new Exception("sp_action_book_copy returned error on ADD.");
    }

    public async Task UpdateAsync(BookCopy copy, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var result = await conn.QuerySingleAsync<SpResult>(
            "CALL sp_action_book_copy(@v_out, @p_book_copies_id, @p_book_id, @p_barcode, " +
            "@p_book_status_id, @p_shelf_type_id, @p_shelf_location, " +
            "@p_acquisition_date, @p_condition, @p_notes, @p_action)",
            new
            {
                v_out              = 0,
                p_book_copies_id   = copy.Id,
                p_book_id          = copy.BookId,
                p_barcode          = copy.Barcode,
                p_book_status_id   = copy.BookStatusId,
                p_shelf_type_id    = copy.ShelfTypeId,
                p_shelf_location   = copy.ShelfLocation,
                p_acquisition_date = copy.AcquisitionDate,
                p_condition        = copy.Condition.ToString(),
                p_notes            = copy.Notes,
                p_action           = "UPDATE"
            });

        if (result.v_out == 99)
            throw new Exception("sp_action_book_copy returned error on UPDATE.");
    }

    public async Task DeleteAsync(long id, CancellationToken ct = default)
    {
        using var conn = db.CreateConnection();
        var result = await conn.QuerySingleAsync<SpResult>(
            "CALL sp_action_book_copy(@v_out, @p_book_copies_id, @p_book_id, @p_barcode, " +
            "@p_book_status_id, @p_shelf_type_id, @p_shelf_location, " +
            "@p_acquisition_date, @p_condition, @p_notes, @p_action)",
            new
            {
                v_out              = 0,
                p_book_copies_id   = id,
                p_book_id          = (long?)null,
                p_barcode          = (string?)null,
                p_book_status_id   = (long?)null,
                p_shelf_type_id    = (long?)null,
                p_shelf_location   = (string?)null,
                p_acquisition_date = (DateTime?)null,
                p_condition        = (string?)null,
                p_notes            = (string?)null,
                p_action           = "DELETE"
            });

        if (result.v_out == 99)
            throw new Exception("sp_action_book_copy returned error on DELETE.");
    }
}