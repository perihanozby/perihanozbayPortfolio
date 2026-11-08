using Microsoft.AspNetCore.Mvc;

namespace perihanozbayPortfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultHeroComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
