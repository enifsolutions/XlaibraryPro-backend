using XlibraryPro.Application.Features.Books.DTOs;

namespace XlibraryPro.Application.Common.Interfaces;

public interface IBookFileParser
{
    /// <summary>
    /// Parse an Excel (.xlsx) or CSV (.csv) stream into a list of rows.
    /// </summary>
    Task<List<BookUploadRowDto>> ParseAsync(Stream fileStream, string fileName, CancellationToken ct = default);
}