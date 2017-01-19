namespace Cloudify.HelperClasses
{
    /// <summary>
    /// The interface definition of the Request Helper class.
    /// </summary>
    public interface IRequestHelper
    {
        /// <summary>
        /// Given a url string, gives you a string representation
        /// of the content at that resource.
        /// </summary>
        /// <param name="url">The specified url.</param>
        /// <returns>The content served from the url.</returns>
        string Get(string url);
    }
}
