using Application.Features.Commands.book.CreateBook;
using Application.Features.Commands.book.DeleteBook;
using Application.Features.Commands.book.UpdateBook;
using Application.Features.Queries.GetBookList;
using Application.Features.Queries.GetBookList.GetAllBooks;
using Application.Features.Queries.GetBookList.GetBookByAverageRating;
using Application.Features.Queries.GetBookList.GetBookByTitle;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles ="Author")]
        [HttpPost("CreateBook")]
        public async Task<ActionResult> CreateBook([FromBody] CreateBookCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Roles ="Author")]
        [HttpPut("UpdateBook")]
        public async Task<ActionResult> UpdateBook([FromBody] UpdateBookCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Roles ="Author")]
        [Authorize(Roles ="Admin")]
        [HttpDelete("DeleteBook/{ISBN}")]
        public async Task<ActionResult> DeleteBook(string ISBN)
        {
           var command = new DeleteBookCommand() { ISBN   = ISBN};
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("GetBookByRating")]
        public async Task<ActionResult<IEnumerable<BookVM>>> GetBooksByRating(decimal rating)
        {
            var command = new GetBookByAverageRatingQuery(rating);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("GetAllBooks")]
        public async Task<ActionResult<IEnumerable<BookVM>>> GetAllBook()
        {
            var command = new AllBooksQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("GetByTitle")]
        public async Task<ActionResult<IEnumerable<BookVM>>> GetByTitle(string title)
        {
            var command = new GetBookListQuery(title);
            var result = await _mediator.Send(command);
            return Ok(result);
        }


    }
}
