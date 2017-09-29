using Matrix.Firewall.Web.Filters;
using System.Configuration;
using System.Web.Mvc;

namespace Matrix.Firewall.Web.Controllers
{
    [LogRequest]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Api = ConfigurationManager.AppSettings["matrix.firewall.api.url"];

            return View();
        }

        public ActionResult Splash()
        {
            return PartialView();
        }

        public ActionResult CountryRestrictions()
        {
            return PartialView();
        }

        public ActionResult AddressRestrictions()
        {
            return PartialView();
        }

        public ActionResult DateTimeRestrictions()
        {
            return PartialView();
        }
    }
}