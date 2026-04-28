using XlibraryPro.Domain.Common;

namespace XlibraryPro.Domain.Entities;

/// <summary>
/// Maps to: tblBook_status (book_status_id PK, book_status Short Text)
/// </summary>
public class BookStatus : BaseEntity
{
    public string Status { get; private set; }

    private BookStatus() : base() { Status = string.Empty; }

    public BookStatus(long id, string status) : base(id)
    {
        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Status cannot be empty.", nameof(status));
        Status = status.Trim();
    }

    public override string ToString() => Status;
}