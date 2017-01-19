using System.Web.Mvc;

namespace Cloudify.Controllers
{
    /// <summary>
    /// The controller for homepage requests.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// GETs the homepage.
        /// </summary>
        /// <returns>The homepage view.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}