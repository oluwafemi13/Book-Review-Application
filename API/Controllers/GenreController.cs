using Application.Features.Commands.genre.CreateGenre;
using Application.Features.Commands.genre.DeleteGenre;
using Application.Features.Commands.genre.UpdateGenre;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class GenreController:ControllerBase
    {
        private readonly IMediator _mediator;
        public GenreController(IMediator mediator)
        {
            _mediator= mediator;
        }

        [HttpPost("CreateGenre")]
        public async Task<ActionResult> CreateGenre([FromBody] CreateGenreCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("UpdateGenre")]
        public async Task<ActionResult> UpdateGenre([FromBody] UpdateGenreCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("DeleteGenre/{Id}")]
        public async Task<ActionResult> DeleteGenre(int id)
        {
            var command = new DeleteGenreCommand { GenreId= id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
