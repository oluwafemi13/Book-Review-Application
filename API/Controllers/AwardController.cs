using Application.Features.Commands.award.DeleteAward;
using Application.Features.Commands.award.UpdateAward;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AwardController: ControllerBase
    {

        private IMediator _mediatR;
        public AwardController(IMediator mediatr)
        {
            _mediatR = mediatr;
        }


        [Authorize(Roles = "Author")]
        [HttpPost("CreateAward")]
        public async Task<ActionResult> CreateAward([FromBody] CreateAwardCommand command)
        {
            var result = await _mediatR.Send(command);
            return Ok(result);
        }

        [Authorize(Roles = "Author")]
        [HttpPut("UpdateAward")]
        public async Task<ActionResult> UpdateAward([FromBody] UpdateAwardCommand command)
        {
            var result = await _mediatR.Send(command);
            return Ok(result);
        }

        [Authorize(Roles = "Author")]
        [HttpDelete("DeleteAward/{Id}")]
        public async Task<ActionResult> DeleteAward(int Id)
        {
            var command = new DeleteAwardCommand() { AwardId = Id };
            var result = await _mediatR.Send(command);
            return Ok(result);
        }

    }
}
