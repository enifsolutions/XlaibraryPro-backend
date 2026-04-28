using XlibraryPro.Domain.Common;

namespace XlibraryPro.Domain.Entities;

/// <summary>Maps to: tblLanguage (language_id PK, language Short Text)</summary>
public class Language : BaseEntity
{
    public string Name { get; private set; }

    private Language() : base() { Name = string.Empty; }

    public Language(long id, string name) : base(id)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Language name cannot be empty.", nameof(name));
        Name = name.Trim();
    }

    public override string ToString() => Name;
}

/// <summary>Maps to: tblBook_type (book_type_id PK, book_type Short Text)</summary>
public class BookTypeEntity : BaseEntity
{
    public string BookType { get; private set; }

    private BookTypeEntity() : base() { BookType = string.Empty; }

    public BookTypeEntity(long id, string bookType) : base(id)
    {
        if (string.IsNullOrWhiteSpace(bookType))
            throw new ArgumentException("Book type cannot be empty.", nameof(bookType));
        BookType = bookType.Trim();
    }

    public override string ToString() => BookType;
}

/// <summary>
/// Dewey Decimal Classification.
/// Maps to: tblDewey_class (dewey_id PK, dewey_number Short Text, dewey_caption Short Text)
/// </summary>
public class DeweyClass : BaseEntity
{
    public string DeweyNumber  { get; private set; }
    public string DeweyCaption { get; private set; }

    private DeweyClass() : base() { DeweyNumber = string.Empty; DeweyCaption = string.Empty; }

    public DeweyClass(long id, string deweyNumber, string deweyCaption) : base(id)
    {
        if (string.IsNullOrWhiteSpace(deweyNumber))
            throw new ArgumentException("Dewey number cannot be empty.", nameof(deweyNumber));
        if (string.IsNullOrWhiteSpace(deweyCaption))
            throw new ArgumentException("Dewey caption cannot be empty.", nameof(deweyCaption));
        DeweyNumber  = deweyNumber.Trim();
        DeweyCaption = deweyCaption.Trim();
    }

    public override string ToString() => $"{DeweyNumber} — {DeweyCaption}";
}

/// <summary>Maps to: tblPublisher (publisher_id PK, publisher_name Short Text)</summary>
public class Publisher : BaseEntity
{
    public string Name { get; private set; }

    private Publisher() : base() { Name = string.Empty; }

    public Publisher(long id, string name) : base(id)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Publisher name cannot be empty.", nameof(name));
        Name = name.Trim();
    }

    public override string ToString() => Name;
}