using MediatR;
using Microsoft.AspNetCore.Mvc;
using XlibraryPro.Application.Features.Books.Commands.BulkUploadBooks;
using XlibraryPro.Application.Features.Books.Commands.CreateBook;
using XlibraryPro.Application.Features.Books.Commands.DeleteBook;
using XlibraryPro.Application.Features.Books.Commands.UpdateBook;
using XlibraryPro.Application.Features.Books.DTOs;
using XlibraryPro.Application.Features.Books.Queries.GetBookById;
using XlibraryPro.Application.Features.Books.Queries.GetBooks;
using XlibraryPro.Application.Features.Books.Queries.SearchBooks;

namespace XlibraryPro.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(IMediator mediator) : ControllerBase
{
    // GET api/books
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BookDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await mediator.Send(new GetBooksQuery(), ct);
        return Ok(result);
    }

    // GET api/books/123
    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(BookDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(long id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetBookByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // GET api/books/search?term=harry
    [HttpGet("search")]
    [ProducesResponseType(typeof(IEnumerable<BookDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Search([FromQuery] string term, CancellationToken ct)
    {
        var result = await mediator.Send(new SearchBooksQuery(term), ct);
        return Ok(result);
    }

    // POST api/books
    [HttpPost]
    [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateBookCommand cmd, CancellationToken ct)
    {
        var id = await mediator.Send(cmd, ct);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    // PUT api/books/123
    [HttpPut("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateBookCommand cmd, CancellationToken ct)
    {
        if (id != cmd.Id) return BadRequest("ID mismatch.");
        await mediator.Send(cmd, ct);
        return NoContent();
    }

    // DELETE api/books/123
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(long id, CancellationToken ct)
    {
        await mediator.Send(new DeleteBookCommand(id), ct);
        return NoContent();
    }

    // POST api/books/bulk-upload
    [HttpPost("bulk-upload")]
    [ProducesResponseType(typeof(BulkUploadResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> BulkUpload(IFormFile file, CancellationToken ct)
    {
        if (file is null || file.Length == 0)
            return BadRequest("No file provided.");

        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (ext is not ".xlsx" and not ".csv")
            return BadRequest("Only .xlsx and .csv files are supported.");

        if (file.Length > 10 * 1024 * 1024)
            return BadRequest("File size must not exceed 10 MB.");

        using var stream = file.OpenReadStream();
        var result = await mediator.Send(new BulkUploadBooksCommand(stream, file.FileName), ct);
        return Ok(result);
    }
}