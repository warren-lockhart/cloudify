using System.Collections.Generic;
using System.Linq;
using Cloudify.HelperClasses;

namespace Cloudify.BusinessLogic
{
    /// <summary>
    /// This class defines methods for parsing html for its word content.
    /// </summary>
    public class WordService : IWordService
    {
        /// <summary>
        /// Gets or sets the Request Helper.
        /// </summary>
        private IRequestHelper _requestHelper { get; set; }

        /// <summary>
        /// The default constructor for this class.
        /// </summary>
        public WordService()
            : this(new RequestHelper())
        { }

        /// <summary>
        /// The constructor which accepts the dependencies of this class.
        /// </summary>
        /// <param name="requestHelper">The request helper dependency.</param>
        public WordService(IRequestHelper requestHelper)
        {
            _requestHelper = requestHelper;
        }

        /// <summary>
        /// Gets a collection of words from a web resource
        /// at the provided url. The top 30 words with 4 or more characters
        /// are returned.
        /// </summary>
        /// <param name="url">The specified url.</param>
        /// <returns>The collection of words.</returns>
        public IEnumerable<string> GetWords(string url)
        {
            string html = _requestHelper.Get(url);

            // We don't bother parsing an empty html string.
            if (string.IsNullOrWhiteSpace(html))
            {
                return Enumerable.Empty<string>();
            }

            // TODO: parse html into words.

            return Enumerable.Empty<string>();
        }
    }
}