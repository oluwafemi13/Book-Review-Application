using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        protected readonly IMediator _mediatr;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public ApiController(IMediator mediatr)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            _mediatr = mediatr;
        }
    }
}
