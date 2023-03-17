using Application.Features.Commands.genre.CreateGenre;
using Application.Features.Commands.genre.DeleteGenre;
using Application.Features.Commands.genre.UpdateGenre;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController:ControllerBase
    {
        private readonly IMediator _mediator;
        public GenreController(IMediator mediator)
        {
            _mediator= mediator;
        }

        [Authorize(Roles = "Author")]
        [Authorize(Roles ="Admin")]
        [HttpPost("CreateGenre")]
        public async Task<ActionResult> CreateGenre([FromBody] CreateGenreCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Roles = "Author")]
        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateGenre")]
        public async Task<ActionResult> UpdateGenre([FromBody] UpdateGenreCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Roles = "Author")]
        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteGenre/{Id}")]
        public async Task<ActionResult> DeleteGenre(int Id)
        {
            var command = new DeleteGenreCommand { GenreId= Id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
