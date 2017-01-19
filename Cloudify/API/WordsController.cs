using System.Web.Http;

namespace Cloudify.API
{
    /// <summary>
    /// This is the Web API controller for Words requests.
    /// </summary>
    [RoutePrefix("api/v1/words")]
    public class WordsController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetWords(string url = "")
        {
            // A url string must be provided.
            if (string.IsNullOrWhiteSpace(url))
            {
                return BadRequest();
            }

            // TODO: process request.

            return Ok();
        }
    }
}