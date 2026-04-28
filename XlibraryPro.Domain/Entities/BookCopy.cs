using XlibraryPro.Domain.Common;
using XlibraryPro.Domain.Enums;

namespace XlibraryPro.Domain.Entities;

/// <summary>
/// Represents a single physical copy of a book.
/// Maps to: tblbook_copies
/// </summary>
public class BookCopy : BaseEntity, IAuditableEntity
{
    public string        Barcode         { get; private set; }
    public long          BookId          { get; private set; }
    public long          BookStatusId    { get; private set; }
    public long          ShelfTypeId     { get; private set; }
    public string        ShelfLocation   { get; private set; }
    public DateTime      AcquisitionDate { get; private set; }
    public BookCondition Condition       { get; private set; }
    public string?       Notes           { get; private set; }

    public DateTime  CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public Book?            Book      { get; private set; }
    public BookStatus?      Status    { get; private set; }
    public ShelfTypeEntity? ShelfType { get; private set; }

    private BookCopy() : base() { Barcode = string.Empty; ShelfLocation = string.Empty; }

    public BookCopy(long id, long bookId, string barcode, long bookStatusId, long shelfTypeId,
                    string shelfLocation, BookCondition condition, DateTime acquisitionDate,
                    string? notes = null)
        : base(id)
    {
        if (bookId <= 0) throw new ArgumentException("Invalid book ID.", nameof(bookId));
        BookId = bookId;
        SetBarcode(barcode);
        SetLocation(shelfTypeId, shelfLocation);
        UpdateStatus(bookStatusId);
        Condition       = condition;
        AcquisitionDate = acquisitionDate.Date == default ? DateTime.Today : acquisitionDate.Date;
        Notes           = notes?.Trim();
        CreatedAt       = DateTime.UtcNow;
    }

    public void SetBarcode(string barcode)
    {
        if (string.IsNullOrWhiteSpace(barcode))
            throw new ArgumentException("Barcode cannot be empty.", nameof(barcode));
        Barcode = barcode.Trim();
        Touch();
    }

    public void SetLocation(long shelfTypeId, string shelfLocation)
    {
        if (shelfTypeId <= 0) throw new ArgumentException("Invalid shelf type ID.", nameof(shelfTypeId));
        if (string.IsNullOrWhiteSpace(shelfLocation))
            throw new ArgumentException("Shelf location cannot be empty.", nameof(shelfLocation));
        ShelfTypeId   = shelfTypeId;
        ShelfLocation = shelfLocation.Trim();
        Touch();
    }

    public void UpdateStatus(long bookStatusId)
    {
        if (bookStatusId <= 0) throw new ArgumentException("Invalid book status ID.", nameof(bookStatusId));
        BookStatusId = bookStatusId;
        Touch();
    }

    public void UpdateCondition(BookCondition condition) { Condition = condition; Touch(); }
    public void UpdateNotes(string? notes) { Notes = notes?.Trim(); Touch(); }

    public bool IsAvailable =>
        Status?.Status?.Equals("Available", StringComparison.OrdinalIgnoreCase) == true;

    private void Touch() { if (Id > 0) UpdatedAt = DateTime.UtcNow; }

    public override string ToString() => $"Copy [{Barcode}] of Book {BookId}";
}