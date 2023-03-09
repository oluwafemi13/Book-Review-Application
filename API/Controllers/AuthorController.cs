using Application.Features.Commands.author.CreateAuthor;
using Application.Features.Commands.author.DeleteAuthor;
using Application.Features.Commands.author.UpdateAuthor;
using Application.Model;
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

        [HttpPost("CreateAuthor")]
        public async Task<ActionResult> CreateAuthor([FromBody] CreateAuthorCommand command)
        {
            var result = await _mediatr.Send(command);
            return Ok(result);
        }

        [HttpPut("UpdateAuthor")]
        public async Task<ActionResult<Response>> UpdateAuthor([FromBody] UpdateAuthorCommand command)
        {
            var result = await _mediatr.Send(command);
            return Ok(result);
        }

        [HttpDelete("DeleteAuthor")]
        public async Task<ActionResult> DeleteAuthor(DeleteAuthorCommand command)
        {
            var result = await _mediatr.Send(command);
            return Ok(result);
        }

    }
}
