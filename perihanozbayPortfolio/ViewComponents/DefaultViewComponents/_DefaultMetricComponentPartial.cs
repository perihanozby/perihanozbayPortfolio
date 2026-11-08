using Microsoft.AspNetCore.Mvc;

namespace perihanozbayPortfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultMetricComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
