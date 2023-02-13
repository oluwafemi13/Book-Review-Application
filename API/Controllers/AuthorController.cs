using Application.Features.Commands.author.CreateAuthor;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController:ControllerBase
    {
        private readonly IMediator _mediatr;

        public AuthorController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAuthor([FromBody] CreateAuthorCommand command)
        {
            var result = await _mediatr.Send(command);
            return Ok(result);
        }

    }
}
