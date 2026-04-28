using MediatR;
using XlibraryPro.Application.Features.Books.DTOs;

namespace XlibraryPro.Application.Features.Books.Commands.BulkUploadBooks;

public record BulkUploadBooksCommand(
    Stream FileStream,
    string FileName
) : IRequest<BulkUploadResultDto>;