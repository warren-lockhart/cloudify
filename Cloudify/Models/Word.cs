namespace Cloudify.Models
{
    /// <summary>
    /// This class models a word in the word cloud.
    /// </summary>
    public class Word
    {
        /// <summary>
        /// Gets or sets the word text.
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// Gets or sets the relative weight of the word.
        /// </summary>
        public int weight { get; set; }
    }
}