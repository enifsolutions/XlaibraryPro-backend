using MediatR;
using XlibraryPro.Application.Common.Interfaces;
using XlibraryPro.Application.Features.Books.DTOs;
using XlibraryPro.Domain.Entities;
using XlibraryPro.Domain.Interfaces;

namespace XlibraryPro.Application.Features.Books.Commands.BulkUploadBooks;

public class BulkUploadBooksHandler(
    IBookFileParser        parser,
    IBookRepository        bookRepo,
    IBulkBookLookupService lookup)
    : IRequestHandler<BulkUploadBooksCommand, BulkUploadResultDto>
{
    public async Task<BulkUploadResultDto> Handle(BulkUploadBooksCommand cmd, CancellationToken ct)
    {
        var result = new BulkUploadResultDto();

        // ── 1. Parse file ────────────────────────────────────────────────────
        List<BookUploadRowDto> rows;
        try
        {
            rows = await parser.ParseAsync(cmd.FileStream, cmd.FileName, ct);
        }
        catch (Exception ex)
        {
            result.Errors.Add(new BulkUploadErrorDto
            {
                RowNumber = 0,
                Reason    = $"File parse failed: {ex.Message}"
            });
            result.Failed = 1;
            return result;
        }

        result.TotalRows = rows.Count;

        // ── 2. Process each row ──────────────────────────────────────────────
        foreach (var row in rows)
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrWhiteSpace(row.Title))
                {
                    AddError(result, row, "Title is required.");
                    continue;
                }

                // Check duplicate ISBN
                if (!string.IsNullOrWhiteSpace(row.Isbn))
                {
                    var exists = await bookRepo.IsbnExistsAsync(row.Isbn, null, ct);
                    if (exists)
                    {
                        result.Skipped++;
                        result.Errors.Add(new BulkUploadErrorDto
                        {
                            RowNumber = row.RowNumber,
                            Title     = row.Title,
                            Isbn      = row.Isbn ?? string.Empty,
                            Reason    = $"Duplicate ISBN: {row.Isbn}"
                        });
                        continue;
                    }
                }

                // Resolve / auto-create lookup IDs
                var languageId  = await lookup.GetOrCreateLanguageAsync(row.Language  ?? "Unknown", ct);
                var bookTypeId  = await lookup.GetOrCreateBookTypeAsync(row.BookType  ?? "Book",    ct);
                var publisherId = await lookup.GetOrCreatePublisherAsync(row.PublisherName ?? "Unknown", ct);
                var deweyId     = await lookup.GetOrCreateDeweyClassAsync(row.DeweyNumber ?? "000", ct);

                // Create book entity
                var book = new Book(
                    id:                 0,
                    title:              row.Title.Trim(),
                    primaryLanguageId:  languageId,
                    bookTypeId:         bookTypeId,
                    deweyId:            deweyId,
                    publisherId:        publisherId,
                    isbn:               row.Isbn,
                    description:        row.Discription,
                    pages:              row.Pages,
                    placeOfPublication: row.PlaceOfPublication,
                    publicationYear:    row.PublicationYear,
                    copyrightYear:      row.CopyrightYear,
                    editionStatement:   row.EditionStatement,
                    notes:              row.Notes
                );

                await bookRepo.AddAsync(book, ct);

                // Resolve authors
                if (!string.IsNullOrWhiteSpace(row.Authors))
                {
                    var authorNames = row.Authors.Split(';', StringSplitOptions.RemoveEmptyEntries);
                    foreach (var fullName in authorNames)
                    {
                        var parts      = fullName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        var firstName  = parts.FirstOrDefault() ?? fullName.Trim();
                        var lastName   = parts.Length > 1 ? parts.Last() : "Unknown";
                        var middleName = parts.Length > 2 ? string.Join(" ", parts[1..^1]) : null;

                        var authorId = await lookup.FindAuthorByNameAsync(fullName.Trim(), ct)
                                    ?? await lookup.CreateAuthorAsync(firstName, middleName, lastName, ct);

                        book.AddAuthor(authorId);
                    }
                }

                // Resolve genres
                if (!string.IsNullOrWhiteSpace(row.Genres))
                {
                    var genreNames = row.Genres.Split(';', StringSplitOptions.RemoveEmptyEntries);
                    foreach (var genreName in genreNames)
                    {
                        var genreId = await lookup.GetOrCreateGenreAsync(genreName.Trim(), ct);
                        book.AddGenre(genreId);
                    }
                }

                result.Inserted++;
            }
            catch (Exception ex)
            {
                AddError(result, row, ex.Message);
            }
        }

        return result;
    }

    private static void AddError(BulkUploadResultDto result, BookUploadRowDto row, string reason)
    {
        result.Failed++;
        result.Errors.Add(new BulkUploadErrorDto
        {
            RowNumber = row.RowNumber,
            Title     = row.Title,
            Isbn      = row.Isbn ?? string.Empty,
            Reason    = reason
        });
    }
}