using Application.Features.Commands.author.CreateAuthor;
using Application.Features.Commands.author.DeleteAuthor;
using Application.Features.Commands.author.UpdateAuthor;
using Application.Features.Commands.AverageRating.CreateAverageRating;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Summary
    /// </summary>

    [ApiController]
    [Route("api/[controller]")]
    public class AverageRatingController:ApiController
    {
        public AverageRatingController(IMediator mediatr) : base(mediatr) { }

        /// <summary>
        /// Summary
        /// </summary>
        //[Authorize(Roles = "User")]
        [HttpPost("CreateAverageRating")]
        public async Task<ActionResult> Create([FromBody] CreateAverageRatingCommand command)
        {
            var result = await _mediatr.Send(command);
            return Ok(result);
        }
    }
}
