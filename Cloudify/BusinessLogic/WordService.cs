using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Cloudify.HelperClasses;
using CsQuery;

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
        /// Given a tag, class or id (a JQuery style selector),
        /// and a string of html, gets the inner content of all
        /// instances in the html and returns the content as a collection.
        /// </summary>
        /// <param name="html">The html</param>
        /// <param name="selector">The selector</param>
        /// <returns>The collection of content.</returns>
        private IEnumerable<string> GetContent(string html, string selector)
        {
            CQ dom = html;
            CQ result = dom[selector];

            return result.ToList().Select(x => x.InnerHTML);
        }

        /// <summary>
        /// Takes a collection of content extracted from a web page,
        /// and removes any remaining html tags and punctuation.
        /// </summary>
        /// <param name="content">The content collection.</param>
        /// <returns>The cleaned content.</returns>
        private IEnumerable<string> CleanContent(IEnumerable<string> content)
        {
            content = content.Select(x =>
            {
                // Remove html tags <> and bulletin board style tags []
                x = Regex.Replace(x, "<.*?>", string.Empty);
                x = Regex.Replace(x, @"\[.*?\]", string.Empty);

                // Remove html entities
                x = Regex.Replace(x, "&.*;", " ");

                // Remove question marks and other punctuation.
                string punctuation = Regex.Escape(".,:;?!-()");
                string pattern = string.Format("[{0}]", punctuation);
                x = Regex.Replace(x, pattern, string.Empty);

                return x;
            });          

            return content;
        }

        /// <summary>
        /// Takes a collection of content extracted from a web page,
        /// splits each content string into a collection of words,
        /// and flattens the result into a single words collection.
        /// </summary>
        /// <param name="content">The content collection.</param>
        /// <returns>The list of words.</returns>
        private IEnumerable<string> GetWords(IEnumerable<string> content)
        {
            IEnumerable<string[]> split = content.Select(p =>
            {
                return Regex.Split(p, @"\s+");
            });

            IEnumerable<string> words = split.SelectMany(x => x);

            // Return all words less than 4 characters
            return words.Where(x => x.Length >= 4);
        }

        /// <summary>
        /// From an html string, extracts all content from inside tags
        /// with a particular selector, and generates a list of words. 
        /// </summary>
        /// <param name="html">The html.</param>
        /// <param name="selector">The Jquery style selector.</param>
        /// <returns>The collection of words.</returns>
        private IEnumerable<string> GetWordsForSelector(string html, string selector)
        {
            // Get all content from inside tags with the provided selector.
            IEnumerable<string> paragraphs = GetContent(html, selector);

            // Remove any html tags from the strings, to deal with things like strong, span etc.
            paragraphs = CleanContent(paragraphs);

            // Split each paragraph into words and flatten
            return GetWords(paragraphs);
        }

        /// <summary>
        /// For a collection of items, keeps only
        /// the top n most occurring items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="num">The ranking criterion.</param>
        /// <returns>The reduced size collection.</returns>
        private IEnumerable<T> GetTopOccurring<T>(IEnumerable<T> items, int num)
        {
            // Remove all but top 'num' most occurring words.
            var grouped = items.GroupBy(word => word).OrderByDescending(word => word.Count()).Take(num);

            // Flatten the grouping back into a collection.
            return grouped.SelectMany(group => group);
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

            List<string> words = new List<string>();

            // Get all words from p tags.
            IEnumerable<string> pWords = GetWordsForSelector(html, "p");
            // Get all words from a tags.
            IEnumerable<string> aWords = GetWordsForSelector(html, "a");
            // Get all words from header tags.
            IEnumerable<string> hWords = GetWordsForSelector(html, "h1,h2,h3,h4,h5,h6");

            // Concatenate the lists.
            words.AddRange(pWords);
            words.AddRange(aWords);
            words.AddRange(hWords);

            // Remove all but top 30 most occurring words.
            return GetTopOccurring(words, 30);
        }
    }
}