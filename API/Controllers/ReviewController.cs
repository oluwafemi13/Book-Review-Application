using Application.Features.Commands.rating.CreateRating;
using Application.Features.Commands.rating.DeleteRating;
using Application.Features.Commands.rating.UpdateRating;
using Application.Features.Commands.Reviews.CreateReview;
using Application.Features.Commands.Reviews.DeleteReview;
using Application.Features.Commands.Reviews.UpdateReview;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController:ControllerBase
    {
        private readonly IMediator _mediator;
        public ReviewController(IMediator mediator)
        {
            _mediator= mediator;
        }

        [HttpPost("CreateReview")]
        public async Task<ActionResult> CreateReview([FromBody] CreateReviewCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("UpdateReview")]
        public async Task<ActionResult> UpdateReview([FromBody] UpdateReviewCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("DeleteReview/{Id}")]
        public async Task<ActionResult> DeleteReview(int ReviewId)
        {
            var command = new DeleteReviewCommand { ReviewId = ReviewId };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
