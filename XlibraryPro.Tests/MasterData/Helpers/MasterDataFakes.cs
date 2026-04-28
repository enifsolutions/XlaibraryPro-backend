using XlibraryPro.Domain.Models.MasterData;

namespace XlibraryPro.Tests.MasterData.Helpers;

public static class MasterDataFakes
{
    public static LanguageModel    Language(long id = 1,  string name = "English")     => new() { LanguageId    = id, Language    = name };
    public static BookTypeModel    BookType(long id = 1,  string name = "General")     => new() { BookTypeId    = id, BookType    = name };
    public static BookStatusModel  BookStatus(long id = 1, string name = "Available") => new() { BookStatusId  = id, BookStatus  = name };
    public static ShelfTypeModel   ShelfType(long id = 1,  string name = "Standard")  => new() { ShelfTypeId   = id, ShelfType   = name };
    public static PublisherModel   Publisher(long id = 1,  string name = "Oxford UP") => new() { PublisherId   = id, PublisherName = name };
    public static GenreModel       Genre(long id = 1,  string name = "Fiction")        => new() { GenreFormId   = id, GenreFormName = name };
    public static LoanStatusModel  LoanStatus(long id = 1,  string name = "Active")   => new() { LoanStatusId  = id, LoanStatus  = name };
    public static MemberStatusModel MemberStatus(long id = 1, string name = "Active") => new() { MemberStatusId = id, Status     = name };
    public static ColourCodeModel  ColourCode(long id = 1,  string colour = "Red", int order = 1) => new() { ColourCodeId = id, Colour = colour, RotationalOrder = order };
    public static DeweyClassModel  DeweyClass(long id = 1,  string number = "000", string caption = "Computer Science") => new() { DeweyId = id, DeweyNumber = number, DeweyCaption = caption };
    public static AuthorModel      Author(long id = 1,  string first = "Arthur", string last = "Clarke") => new() { AuthorId = id, FirstName = first, LastName = last, Dates = "1917-2008" };
    public static StudentBatchModel StudentBatch(long id = 1, string year = "2024", long colourId = 1, int maxBooks = 5) => new() { StudentBatchId = id, SchoolYear = year, ColourCodeId = colourId, MaxBooksAllowed = maxBooks };
}