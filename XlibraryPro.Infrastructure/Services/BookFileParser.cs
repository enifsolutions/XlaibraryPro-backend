using CsvHelper;
using CsvHelper.Configuration;
using OfficeOpenXml;
using System.Globalization;
using XlibraryPro.Application.Common.Interfaces;
using XlibraryPro.Application.Features.Books.DTOs;

namespace XlibraryPro.Infrastructure.Services;

public class BookFileParser : IBookFileParser
{
    public async Task<List<BookUploadRowDto>> ParseAsync(Stream fileStream, string fileName, CancellationToken ct = default)
    {
        var ext = Path.GetExtension(fileName).ToLowerInvariant();
        return ext switch
        {
            ".xlsx" => await ParseExcelAsync(fileStream, ct),
            ".csv"  => await ParseCsvAsync(fileStream, ct),
            _       => throw new NotSupportedException($"File type '{ext}' is not supported. Use .xlsx or .csv.")
        };
    }

    // ── Excel parser ──────────────────────────────────────────────────────────
    private static Task<List<BookUploadRowDto>> ParseExcelAsync(Stream stream, CancellationToken ct)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using var package   = new ExcelPackage(stream);
        var worksheet       = package.Workbook.Worksheets.FirstOrDefault()
                              ?? throw new Exception("Excel file contains no worksheets.");

        var rows    = new List<BookUploadRowDto>();
        var headers = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        // Read header row (row 1)
        for (int col = 1; col <= worksheet.Dimension?.Columns; col++)
        {
            var header = worksheet.Cells[1, col].Text.Trim();
            if (!string.IsNullOrEmpty(header))
                headers[header] = col;
        }

        // Read data rows
        int totalRows = worksheet.Dimension?.Rows ?? 1;
        for (int row = 2; row <= totalRows; row++)
        {
            // Skip completely empty rows
            var titleVal = GetCell(worksheet, row, headers, "title");
            if (string.IsNullOrWhiteSpace(titleVal)) continue;

            rows.Add(new BookUploadRowDto
            {
                RowNumber          = row,
                Title              = titleVal,
                Discription        = GetCell(worksheet, row, headers, "discription"),
                Isbn               = GetCell(worksheet, row, headers, "isbn"),
                Pages              = ParseInt(GetCell(worksheet, row, headers, "pages")),
                Language           = GetCell(worksheet, row, headers, "language"),
                BookType           = GetCell(worksheet, row, headers, "book_type"),
                DeweyNumber        = GetCell(worksheet, row, headers, "dewey_number"),
                PublisherName      = GetCell(worksheet, row, headers, "publisher_name"),
                PlaceOfPublication = GetCell(worksheet, row, headers, "place_of_publication"),
                PublicationYear    = GetCell(worksheet, row, headers, "publication_year"),
                CopyrightYear      = ParseInt(GetCell(worksheet, row, headers, "copyright_year")),
                EditionStatement   = GetCell(worksheet, row, headers, "edition_statement"),
                Notes              = GetCell(worksheet, row, headers, "notes"),
                Authors            = GetCell(worksheet, row, headers, "authors"),
                Genres             = GetCell(worksheet, row, headers, "genres")
            });
        }

        return Task.FromResult(rows);
    }

    // ── CSV parser ────────────────────────────────────────────────────────────
    private static async Task<List<BookUploadRowDto>> ParseCsvAsync(Stream stream, CancellationToken ct)
    {
        using var reader = new StreamReader(stream);
        using var csv    = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HeaderValidated   = null,
            MissingFieldFound = null,
            TrimOptions       = TrimOptions.Trim
        });

        var rows   = new List<BookUploadRowDto>();
        int rowNum = 2; // Row 1 = header

        await csv.ReadAsync();
        csv.ReadHeader();

        while (await csv.ReadAsync())
        {
            var title = csv.GetField("title") ?? string.Empty;
            if (string.IsNullOrWhiteSpace(title)) { rowNum++; continue; }

            rows.Add(new BookUploadRowDto
            {
                RowNumber          = rowNum++,
                Title              = title,
                Discription        = csv.GetField("discription"),
                Isbn               = csv.GetField("isbn"),
                Pages              = ParseInt(csv.GetField("pages")),
                Language           = csv.GetField("language"),
                BookType           = csv.GetField("book_type"),
                DeweyNumber        = csv.GetField("dewey_number"),
                PublisherName      = csv.GetField("publisher_name"),
                PlaceOfPublication = csv.GetField("place_of_publication"),
                PublicationYear    = csv.GetField("publication_year"),
                CopyrightYear      = ParseInt(csv.GetField("copyright_year")),
                EditionStatement   = csv.GetField("edition_statement"),
                Notes              = csv.GetField("notes"),
                Authors            = csv.GetField("authors"),
                Genres             = csv.GetField("genres")
            });
        }

        return rows;
    }

    // ── Helpers ───────────────────────────────────────────────────────────────
    private static string? GetCell(ExcelWorksheet ws, int row, Dictionary<string, int> headers, string column)
    {
        if (!headers.TryGetValue(column, out var col)) return null;
        var val = ws.Cells[row, col].Text.Trim();
        return string.IsNullOrEmpty(val) ? null : val;
    }

    private static int? ParseInt(string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) return null;
        return int.TryParse(value, out var result) ? result : null;
    }
}