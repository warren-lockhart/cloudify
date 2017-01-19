using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using Newtonsoft.Json;

namespace Cloudify.App_Start
{
    /// <summary>
    /// This class is for setup of API action routing.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// This method registers the routes.
        /// </summary>
        /// <param name="config">The global object containing the routes collection</param>
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.EnsureInitialized();

            GlobalConfiguration.Configuration.Formatters.Clear();
            GlobalConfiguration.Configuration.Formatters.Add(new JsonMediaTypeFormatter());
            GlobalConfiguration.Configuration.Formatters.Add(new XmlMediaTypeFormatter());

            var json = config.Formatters.JsonFormatter;
            json.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.Clear();
            config.Formatters.Add(json);

            var jsonFormatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
        }
    }
}