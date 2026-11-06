using Microsoft.AspNetCore.Mvc;

namespace perihanozbayPortfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultNavbarComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
