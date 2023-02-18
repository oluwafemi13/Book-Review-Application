using Application.Features.Commands.author.CreateAuthor;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AwardController:ControllerBase
    {
        private readonly Mediator _mediatR;
       
        public AwardController(Mediator mediatR)
        {
            _mediatR = mediatR;
        }

        [HttpPost("CreateAward")]
        public async Task<ActionResult> CreateAuthor([FromBody] CreateAuthorCommand command)
        {
            var result = await _mediatR.Send(command);
            return Ok(result);
        }

    }
}
