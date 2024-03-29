﻿using Application.DTO;
using Application.Features.Commands.book.CreateBook;
using Application.Features.Commands.book.DeleteBook;
using Application.Features.Commands.book.UpdateBook;
using Application.Features.Queries.GetBookList;
using Application.Features.Queries.GetBookList.GetAll;
using Application.Features.Queries.GetBookList.GetByAuthor;
using Application.Features.Queries.GetBookList.GetByAverageRating;
using Application.Features.Queries.GetBookList.GetByTitle;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private IMediator _mediator;
        public BookController(IMediator mediatr)
        {
            _mediator= mediatr;
        } 

        /*[Authorize(Roles ="Author")]*/
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
        //[ResponseCache(CacheProfileName = "240SecondsCaching")]
        public async Task<ActionResult<IEnumerable<BookVM>>> GetBooksByRating(decimal rating)
        {
            var command = new GetByAverageRatingQuery(rating);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("GetAllBooks")]
        //[ResponseCache(CacheProfileName = "240SecondsCaching")]
        public async Task<ActionResult<IEnumerable<BookVM>>> GetAllBook([FromQuery] RequestParameters request)
        {
            var command = new AllBooksQuery(request.PageIndex, request.PageSize);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("GetByTitle")]
        //[ResponseCache(CacheProfileName = "240SecondsCaching")]
        public async Task<ActionResult<IEnumerable<BookVM>>> GetByTitle(string title)
        {
            var command = new GetBookListQuery(title);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("GetByAuthor")]
        //[ResponseCache(CacheProfileName = "240SecondsCaching")]
        public async Task<ActionResult<IEnumerable<BookVM>>> GetByAuthor(string Author)
        {
            var command = new GetByAuthorQuery(Author);
            var result = await _mediator.Send(command);
            return Ok(result);
        }


    }
}
