using Application.Features.Commands.award.DeleteAward;
using Application.Features.Commands.award.UpdateAward;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AwardController:ControllerBase
    {
        private readonly IMediator _mediatR;
       
        public AwardController(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        [HttpPost("CreateAward")]
        public async Task<ActionResult> CreateAward([FromBody] CreateAwardCommand command)
        {
            var result = await _mediatR.Send(command);
            return Ok(result);
        }

        [HttpPut("UpdateAward")]
        public async Task<ActionResult> UpdateAward([FromBody] UpdateAwardCommand command)
        {
            var result = await _mediatR.Send(command);
            return Ok(result);
        }

        [HttpDelete("DeleteAward")]
        public async Task<ActionResult> DeleteAward([FromBody] DeleteAwardCommand command)
        {
            var result = await _mediatR.Send(command);
            return Ok(result);
        }

    }
}
