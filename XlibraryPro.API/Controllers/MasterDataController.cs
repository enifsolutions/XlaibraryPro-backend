using MediatR;
using Microsoft.AspNetCore.Mvc;
using XlibraryPro.Application.Features.MasterData.Language.Commands.ActionLanguage;
using XlibraryPro.Application.Features.MasterData.Language.Queries.GetAllLanguages;
using XlibraryPro.Application.Features.MasterData.Language.Queries.GetLanguageById;
using XlibraryPro.Application.Features.MasterData.BookType.Commands.ActionBookType;
using XlibraryPro.Application.Features.MasterData.BookType.Queries.GetAllBookTypes;
using XlibraryPro.Application.Features.MasterData.BookType.Queries.GetBookTypeById;
using XlibraryPro.Application.Features.MasterData.BookStatus.Commands.ActionBookStatus;
using XlibraryPro.Application.Features.MasterData.BookStatus.Queries.GetAllBookStatuses;
using XlibraryPro.Application.Features.MasterData.BookStatus.Queries.GetBookStatusById;
using XlibraryPro.Application.Features.MasterData.ShelfType.Commands.ActionShelfType;
using XlibraryPro.Application.Features.MasterData.ShelfType.Queries.GetAllShelfTypes;
using XlibraryPro.Application.Features.MasterData.ShelfType.Queries.GetShelfTypeById;
using XlibraryPro.Application.Features.MasterData.DeweyClass.Commands.ActionDeweyClass;
using XlibraryPro.Application.Features.MasterData.DeweyClass.Queries.GetAllDeweyClasses;
using XlibraryPro.Application.Features.MasterData.DeweyClass.Queries.GetDeweyClassById;
using XlibraryPro.Application.Features.MasterData.Publisher.Commands.ActionPublisher;
using XlibraryPro.Application.Features.MasterData.Publisher.Queries.GetAllPublishers;
using XlibraryPro.Application.Features.MasterData.Publisher.Queries.GetPublisherById;
using XlibraryPro.Application.Features.MasterData.Author.Commands.ActionAuthor;
using XlibraryPro.Application.Features.MasterData.Author.Queries.GetAllAuthors;
using XlibraryPro.Application.Features.MasterData.Author.Queries.GetAuthorById;
using XlibraryPro.Application.Features.MasterData.Genre.Commands.ActionGenre;
using XlibraryPro.Application.Features.MasterData.Genre.Queries.GetAllGenres;
using XlibraryPro.Application.Features.MasterData.Genre.Queries.GetGenreById;
using XlibraryPro.Application.Features.MasterData.LoanStatus.Commands.ActionLoanStatus;
using XlibraryPro.Application.Features.MasterData.LoanStatus.Queries.GetAllLoanStatuses;
using XlibraryPro.Application.Features.MasterData.LoanStatus.Queries.GetLoanStatusById;
using XlibraryPro.Application.Features.MasterData.MemberStatus.Commands.ActionMemberStatus;
using XlibraryPro.Application.Features.MasterData.MemberStatus.Queries.GetAllMemberStatuses;
using XlibraryPro.Application.Features.MasterData.MemberStatus.Queries.GetMemberStatusById;
using XlibraryPro.Application.Features.MasterData.StudentBatch.Commands.ActionStudentBatch;
using XlibraryPro.Application.Features.MasterData.StudentBatch.Queries.GetAllStudentBatches;
using XlibraryPro.Application.Features.MasterData.StudentBatch.Queries.GetStudentBatchById;
using XlibraryPro.Application.Features.MasterData.ColourCode.Commands.ActionColourCode;
using XlibraryPro.Application.Features.MasterData.ColourCode.Queries.GetAllColourCodes;
using XlibraryPro.Application.Features.MasterData.ColourCode.Queries.GetColourCodeById;

namespace XlibraryPro.API.Controllers;

[ApiController]
[Route("api/master")]
public class MasterDataController(IMediator mediator) : ControllerBase
{
    // =========================================================================
    // LANGUAGE
    // =========================================================================
    [HttpGet("languages")]
    public async Task<IActionResult> GetAllLanguages()
        => Ok(await mediator.Send(new GetAllLanguagesQuery()));

    [HttpGet("languages/{id:long}")]
    public async Task<IActionResult> GetLanguageById(long id)
    {
        var result = await mediator.Send(new GetLanguageByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost("languages")]
    public async Task<IActionResult> ActionLanguage([FromBody] ActionLanguageCommand cmd)
    {
        var result = await mediator.Send(cmd);
        return result == 1 ? Ok(new { success = true }) : BadRequest(new { success = false });
    }

    // =========================================================================
    // BOOK TYPE
    // =========================================================================
    [HttpGet("book-types")]
    public async Task<IActionResult> GetAllBookTypes()
        => Ok(await mediator.Send(new GetAllBookTypesQuery()));

    [HttpGet("book-types/{id:long}")]
    public async Task<IActionResult> GetBookTypeById(long id)
    {
        var result = await mediator.Send(new GetBookTypeByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost("book-types")]
    public async Task<IActionResult> ActionBookType([FromBody] ActionBookTypeCommand cmd)
    {
        var result = await mediator.Send(cmd);
        return result == 1 ? Ok(new { success = true }) : BadRequest(new { success = false });
    }

    // =========================================================================
    // BOOK STATUS
    // =========================================================================
    [HttpGet("book-statuses")]
    public async Task<IActionResult> GetAllBookStatuses()
        => Ok(await mediator.Send(new GetAllBookStatusesQuery()));

    [HttpGet("book-statuses/{id:long}")]
    public async Task<IActionResult> GetBookStatusById(long id)
    {
        var result = await mediator.Send(new GetBookStatusByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost("book-statuses")]
    public async Task<IActionResult> ActionBookStatus([FromBody] ActionBookStatusCommand cmd)
    {
        var result = await mediator.Send(cmd);
        return result == 1 ? Ok(new { success = true }) : BadRequest(new { success = false });
    }

    // =========================================================================
    // SHELF TYPE
    // =========================================================================
    [HttpGet("shelf-types")]
    public async Task<IActionResult> GetAllShelfTypes()
        => Ok(await mediator.Send(new GetAllShelfTypesQuery()));

    [HttpGet("shelf-types/{id:long}")]
    public async Task<IActionResult> GetShelfTypeById(long id)
    {
        var result = await mediator.Send(new GetShelfTypeByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost("shelf-types")]
    public async Task<IActionResult> ActionShelfType([FromBody] ActionShelfTypeCommand cmd)
    {
        var result = await mediator.Send(cmd);
        return result == 1 ? Ok(new { success = true }) : BadRequest(new { success = false });
    }

    // =========================================================================
    // DEWEY CLASS
    // =========================================================================
    [HttpGet("dewey-classes")]
    public async Task<IActionResult> GetAllDeweyClasses()
        => Ok(await mediator.Send(new GetAllDeweyClassesQuery()));

    [HttpGet("dewey-classes/{id:long}")]
    public async Task<IActionResult> GetDeweyClassById(long id)
    {
        var result = await mediator.Send(new GetDeweyClassByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost("dewey-classes")]
    public async Task<IActionResult> ActionDeweyClass([FromBody] ActionDeweyClassCommand cmd)
    {
        var result = await mediator.Send(cmd);
        return result == 1 ? Ok(new { success = true }) : BadRequest(new { success = false });
    }

    // =========================================================================
    // PUBLISHER
    // =========================================================================
    [HttpGet("publishers")]
    public async Task<IActionResult> GetAllPublishers()
        => Ok(await mediator.Send(new GetAllPublishersQuery()));

    [HttpGet("publishers/{id:long}")]
    public async Task<IActionResult> GetPublisherById(long id)
    {
        var result = await mediator.Send(new GetPublisherByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost("publishers")]
    public async Task<IActionResult> ActionPublisher([FromBody] ActionPublisherCommand cmd)
    {
        var result = await mediator.Send(cmd);
        return result == 1 ? Ok(new { success = true }) : BadRequest(new { success = false });
    }

    // =========================================================================
    // AUTHOR
    // =========================================================================
    [HttpGet("authors")]
    public async Task<IActionResult> GetAllAuthors()
        => Ok(await mediator.Send(new GetAllAuthorsQuery()));

    [HttpGet("authors/{id:long}")]
    public async Task<IActionResult> GetAuthorById(long id)
    {
        var result = await mediator.Send(new GetAuthorByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost("authors")]
    public async Task<IActionResult> ActionAuthor([FromBody] ActionAuthorCommand cmd)
    {
        var result = await mediator.Send(cmd);
        return result == 1 ? Ok(new { success = true }) : BadRequest(new { success = false });
    }

    // =========================================================================
    // GENRE
    // =========================================================================
    [HttpGet("genres")]
    public async Task<IActionResult> GetAllGenres()
        => Ok(await mediator.Send(new GetAllGenresQuery()));

    [HttpGet("genres/{id:long}")]
    public async Task<IActionResult> GetGenreById(long id)
    {
        var result = await mediator.Send(new GetGenreByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost("genres")]
    public async Task<IActionResult> ActionGenre([FromBody] ActionGenreCommand cmd)
    {
        var result = await mediator.Send(cmd);
        return result == 1 ? Ok(new { success = true }) : BadRequest(new { success = false });
    }

    // =========================================================================
    // LOAN STATUS
    // =========================================================================
    [HttpGet("loan-statuses")]
    public async Task<IActionResult> GetAllLoanStatuses()
        => Ok(await mediator.Send(new GetAllLoanStatusesQuery()));

    [HttpGet("loan-statuses/{id:long}")]
    public async Task<IActionResult> GetLoanStatusById(long id)
    {
        var result = await mediator.Send(new GetLoanStatusByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost("loan-statuses")]
    public async Task<IActionResult> ActionLoanStatus([FromBody] ActionLoanStatusCommand cmd)
    {
        var result = await mediator.Send(cmd);
        return result == 1 ? Ok(new { success = true }) : BadRequest(new { success = false });
    }

    // =========================================================================
    // MEMBER STATUS
    // =========================================================================
    [HttpGet("member-statuses")]
    public async Task<IActionResult> GetAllMemberStatuses()
        => Ok(await mediator.Send(new GetAllMemberStatusesQuery()));

    [HttpGet("member-statuses/{id:long}")]
    public async Task<IActionResult> GetMemberStatusById(long id)
    {
        var result = await mediator.Send(new GetMemberStatusByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost("member-statuses")]
    public async Task<IActionResult> ActionMemberStatus([FromBody] ActionMemberStatusCommand cmd)
    {
        var result = await mediator.Send(cmd);
        return result == 1 ? Ok(new { success = true }) : BadRequest(new { success = false });
    }

    // =========================================================================
    // STUDENT BATCH
    // =========================================================================
    [HttpGet("student-batches")]
    public async Task<IActionResult> GetAllStudentBatches()
        => Ok(await mediator.Send(new GetAllStudentBatchesQuery()));

    [HttpGet("student-batches/{id:long}")]
    public async Task<IActionResult> GetStudentBatchById(long id)
    {
        var result = await mediator.Send(new GetStudentBatchByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost("student-batches")]
    public async Task<IActionResult> ActionStudentBatch([FromBody] ActionStudentBatchCommand cmd)
    {
        var result = await mediator.Send(cmd);
        return result == 1 ? Ok(new { success = true }) : BadRequest(new { success = false });
    }

    // =========================================================================
    // COLOUR CODE
    // =========================================================================
    [HttpGet("colour-codes")]
    public async Task<IActionResult> GetAllColourCodes()
        => Ok(await mediator.Send(new GetAllColourCodesQuery()));

    [HttpGet("colour-codes/{id:long}")]
    public async Task<IActionResult> GetColourCodeById(long id)
    {
        var result = await mediator.Send(new GetColourCodeByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost("colour-codes")]
    public async Task<IActionResult> ActionColourCode([FromBody] ActionColourCodeCommand cmd)
    {
        var result = await mediator.Send(cmd);
        return result == 1 ? Ok(new { success = true }) : BadRequest(new { success = false });
    }
}
