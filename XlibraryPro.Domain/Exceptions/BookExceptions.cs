namespace XlibraryPro.Domain.Exceptions;

public class BookNotFoundException : Exception
{
    public long BookId { get; }
    public BookNotFoundException(long bookId)
        : base($"Book with ID {bookId} was not found.")
        => BookId = bookId;
}

public class BookCopyNotFoundException : Exception
{
    public long CopyId { get; }
    public BookCopyNotFoundException(long copyId)
        : base($"Book copy with ID {copyId} was not found.")
        => CopyId = copyId;
}

public class DuplicateBarcodeException : Exception
{
    public string Barcode { get; }
    public DuplicateBarcodeException(string barcode)
        : base($"A book copy with barcode '{barcode}' already exists.")
        => Barcode = barcode;
}

public class InvalidIsbnException : Exception
{
    public string RawIsbn { get; }
    public InvalidIsbnException(string rawIsbn, string reason)
        : base($"Invalid ISBN '{rawIsbn}': {reason}")
        => RawIsbn = rawIsbn;
}