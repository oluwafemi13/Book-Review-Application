using Application.Features.Commands.book.CreateBook;
using Application.Features.Commands.book.DeleteBook;
using Application.Features.Commands.book.UpdateBook;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController: ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateBook")]
        public async Task<ActionResult> CreateBook([FromBody] CreateBookCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("UpdateBook")]
        public async Task<ActionResult> UpdateBook([FromBody] UpdateBookCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("DeleteBook")]
        public async Task<ActionResult> DeleteBook(Guid Id)
        {
           var command = new DeleteBookCommand() { BookId= Id };
            await _mediator.Send(command);
            return Ok();
        }


    }
}
