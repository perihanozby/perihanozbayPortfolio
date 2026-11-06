using Microsoft.AspNetCore.Mvc;

namespace perihanozbayPortfolio.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
