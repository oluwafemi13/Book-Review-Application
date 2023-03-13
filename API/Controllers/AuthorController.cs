using Application.Features.Commands.author.CreateAuthor;
using Application.Features.Commands.author.DeleteAuthor;
using Application.Features.Commands.author.UpdateAuthor;
using Application.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
//using System.Web.Http;

namespace API.Controllers
{
    /// <summary>
    /// Some countries do not have neither a State, nor a Province
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController:ControllerBase
    {
        private readonly IMediator _mediatr;

        public AuthorController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        /// <summary>
        /// Some countries do not have neither a State, nor a Province
        /// </summary>
        [HttpPost("CreateAuthor")]
        public async Task<ActionResult> CreateAuthor([FromBody] CreateAuthorCommand command)
        {
            var result = await _mediatr.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Some countries do not have neither a State, nor a Province
        /// </summary>
        [HttpPut("UpdateAuthor")]
        public async Task<ActionResult> UpdateAuthor([FromBody] UpdateAuthorCommand command)
        {
            var result = await _mediatr.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Some countries do not have neither a State, nor a Province
        /// </summary>
        [HttpDelete("DeleteAuthor/{Id}")]
        public async Task<ActionResult> DeleteAuthor(int AuthorId)
        {
            var command = new DeleteAuthorCommand { AuthorId = AuthorId };
            var result = await _mediatr.Send(command);
            return Ok(result);
        }

        /*[HttpGet]
        //[Route("api/resources/{id}")]
        public Task<IHttpActionResult> GetResource(int id)
        {
            var resource = _repository.GetResource(id);

            if (resource == null)
            {
                return NotFound();
            }

            // Set the ETag header to a unique value for the resource
            var etag = GenerateETag(resource);
            if (Request.Headers.IfNoneMatch.Any(e => e.Tag == etag))
            {
                // The resource has not been modified since the last request, return a 304 response
                return StatusCode(HttpStatusCode.NotModified);
            }

            // Set the Last-Modified header to the last modified date for the resource
            var lastModified = GetLastModifiedDate(resource);
            if (Request.Headers.IfModifiedSince.HasValue && Request.Headers.IfModifiedSince.Value >= lastModified)
            {
                // The resource has not been modified since the last request, return a 304 response
                return StatusCode(HttpStatusCode.NotModified);
            }

            // Set the ETag and Last-Modified headers in the response
            Response.Headers.ETag = new EntityTagHeaderValue(etag);
            Response.Headers.LastModified = lastModified;

            // Return the resource
            return Ok(resource);
        }

        private string GenerateETag(Resource resource)
        {
            // Generate a unique hash for the resource
            var hash = GetHashCode(resource);
            return hash.ToString("X");
        }

        private DateTimeOffset GetLastModifiedDate(Resource resource)
        {
            // Get the last modified date for the resource
            return resource.LastModified;
        }*/

    }
}
