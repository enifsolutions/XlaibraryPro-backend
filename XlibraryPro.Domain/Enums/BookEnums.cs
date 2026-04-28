namespace XlibraryPro.Domain.Enums;

/// <summary>Maps to tblBook_status.book_status</summary>
public enum BookCopyStatus
{
    Available        = 1,
    OnLoan           = 2,
    Reserved         = 3,
    UnderMaintenance = 4,
    Lost             = 5,
    Withdrawn        = 6
}

/// <summary>Maps to tblShelf_type.shelf_type</summary>
public enum ShelfType
{
    General    = 1,
    Reference  = 2,
    Periodical = 3,
    Rare       = 4,
    Digital    = 5
}

/// <summary>Maps to tblBook_type.book_type</summary>
public enum BookType
{
    Book       = 1,
    Periodical = 2,
    Journal    = 3,
    Magazine   = 4,
    Newspaper  = 5,
    Thesis     = 6,
    Report     = 7
}

/// <summary>Condition of a physical book copy — stored in tblbook_copies.condition</summary>
public enum BookCondition
{
    New     = 1,
    Good    = 2,
    Fair    = 3,
    Poor    = 4,
    Damaged = 5
}