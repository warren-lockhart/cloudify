using System.Collections.Generic;
using Cloudify.Models;

namespace Cloudify.BusinessLogic
{
    /// <summary>
    /// The interface definition for the Word Service.
    /// </summary>
    public interface IWordService
    {
        /// <summary>
        /// Gets a collection of words from a web resource
        /// at the provided url. The top 30 words with 4 or more characters
        /// are returned.
        /// </summary>
        /// <param name="url">The specified url.</param>
        /// <returns>The collection of words.</returns>
        IEnumerable<Word> GetWords(string url);
    }
}
