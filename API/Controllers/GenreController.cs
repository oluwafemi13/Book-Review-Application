using Application.Features.Commands.genre.CreateGenre;
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
    }
}
