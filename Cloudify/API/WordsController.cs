using System.Collections.Generic;
using System.Web.Http;
using Cloudify.BusinessLogic;

namespace Cloudify.API
{
    /// <summary>
    /// This is the Web API controller for Words requests.
    /// </summary>
    [RoutePrefix("api/v1/words")]
    public class WordsController : ApiController
    {
        /// <summary>
        /// Gets or sets the Word Service.
        /// </summary>
        private IWordService _wordService { get; set; }

        /// <summary>
        /// The default constructor for this controller.
        /// </summary>
        public WordsController()
            : this(new WordService())
        { }

        /// <summary>
        /// The constructor which accepts the dependencies of this class.
        /// </summary>
        /// <param name="wordService">The Word Service dependency.</param>
        public WordsController(IWordService wordService)
        {
            _wordService = wordService;
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetWords(string url = "")
        {
            // A url string must be provided.
            if (string.IsNullOrWhiteSpace(url))
            {
                return BadRequest();
            }

            // Fetch the html at the specified url, and get a collection of most used words.
            IEnumerable<string> words = _wordService.GetWords(url);

            return Ok(words);
        }
    }
}