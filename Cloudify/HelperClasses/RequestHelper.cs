using System;
using System.IO;
using System.Net;

namespace Cloudify.HelperClasses
{
    /// <summary>
    /// This class defines methods for helping with HTTP requests.
    /// </summary>
    public class RequestHelper : IRequestHelper
    {
        /// <summary>
        /// Create a Uri object from a uri string.
        /// This is helpful for cases when the protocol part is not provided;
        /// in that case we default to http.
        /// </summary>
        /// <param name="uri">The uri string.</param>
        /// <returns>The Uri object.</returns>
        private Uri GetUri(string uri)
        {
            return new UriBuilder(uri).Uri;
        }

        /// <summary>
        /// Given a url string, gives you a string representation
        /// of the content at that resource.
        /// </summary>
        /// <param name="url">The specified url.</param>
        /// <returns>The content served from the url.</returns>
        public string Get(string url)
        {
            try
            {
                // Create a Uri object to handle urls with missing protocol fragments.
                Uri uri = GetUri(url);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                // If the request responded with a 200 code, return the content from the response.
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string data = reader.ReadToEnd();

                    response.Close();
                    reader.Close();

                    return data;
                }
                else
                {
                    // --> log error
                    return string.Empty;
                }
            }
            catch (Exception)
            {
                // --> log exception
                return string.Empty;
            }
        }
    }
}