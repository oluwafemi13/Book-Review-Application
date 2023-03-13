using Application.Features.Commands.genre.CreateGenre;
using Application.Features.Commands.genre.UpdateGenre;
using Application.Features.Commands.rating.CreateRating;
using Application.Features.Commands.rating.DeleteRating;
using Application.Features.Commands.rating.UpdateRating;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatingController: ControllerBase
    {
        private readonly IMediator _mediator;
        public RatingController(IMediator mediator)
        {
            _mediator= mediator;
        }

        [HttpPost("CreateRating")]
        public async Task<ActionResult> CreateRating([FromBody] CreateRatingCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("UpdateRating")]
        public async Task<ActionResult> UpdateRating([FromBody] UpdateRatingCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("DeleteRating/{Id}")]
        public async Task<ActionResult> DeleteRating(int RatingId)
        {
            var command = new DeleteRatingCommand { RatingId= RatingId };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
