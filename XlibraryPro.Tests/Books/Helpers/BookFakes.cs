using XlibraryPro.Domain.Entities;
using XlibraryPro.Domain.Enums;

namespace XlibraryPro.Tests.Books.Helpers;

/// <summary>
/// Shared fake data factory for Books module tests.
/// </summary>
public static class BookFakes
{
    public static Book MakeBook(
        long   id    = 20240101_00000001L,
        string title = "Clean Code")
    {
        return new Book(
            id:                 id,
            title:              title,
            primaryLanguageId:  1,
            bookTypeId:         1,
            deweyId:            1,
            publisherId:        1,
            isbn:               "9780132350884",
            description:        "A handbook of agile software craftsmanship.",
            pages:              431,
            placeOfPublication: "Upper Saddle River",
            publicationYear:    "2008",
            copyrightYear:      2008,
            editionStatement:   "1st ed.",
            notes:              null
        );
    }

    public static IEnumerable<Book> MakeBooks(int count = 3)
    {
        return Enumerable.Range(1, count).Select(i =>
            MakeBook(id: 20240101_00000000L + i, title: $"Book Title {i}"));
    }

    public static BookCopy MakeCopy(
        long   id     = 20240101_00000099L,
        long   bookId = 20240101_00000001L,
        string barcode = "BC-0001")
    {
        return new BookCopy(
            id:              id,
            bookId:          bookId,
            barcode:         barcode,
            bookStatusId:    1,
            shelfTypeId:     1,
            shelfLocation:   "A1-01",
            condition:       BookCondition.Good,
            acquisitionDate: new DateTime(2024, 1, 1),
            notes:           null
        );
    }
}