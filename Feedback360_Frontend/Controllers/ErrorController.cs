using Microsoft.AspNetCore.Mvc;

namespace Feedback360_Frontend.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        public ActionResult PageNotFoundError()
        {
            return View();
        }

        [HttpGet]
        public ActionResult InternalServerError()
        {
            return View();
        }
    }
}
