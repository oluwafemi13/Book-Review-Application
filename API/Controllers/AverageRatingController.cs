using Application.Features.Commands.author.CreateAuthor;
using Application.Features.Commands.author.DeleteAuthor;
using Application.Features.Commands.author.UpdateAuthor;
using Application.Features.Commands.AverageRating.CreateAverageRating;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Some countries do not have neither a State, nor a Province
    /// </summary>

    [ApiController]
    [Route("api/[controller]")]
    public class AverageRatingController:ControllerBase
    {
        private readonly IMediator _mediatr;

        public AverageRatingController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        /// <summary>
        /// Some countries do not have neither a State, nor a Province
        /// </summary>
        [HttpPost("CreateAverageRating")]
        public async Task<ActionResult> Create([FromBody] CreateAverageRatingCommand command)
        {
            var result = await _mediatr.Send(command);
            return Ok(result);
        }
    }
}
