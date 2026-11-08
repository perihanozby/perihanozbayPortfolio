
using Microsoft.AspNetCore.Mvc;

namespace perihanozbayPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
