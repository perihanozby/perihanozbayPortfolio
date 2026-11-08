using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using perihanozbayPortfolio.Context;

namespace perihanozbayPortfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultHeroComponentPartial:ViewComponent
    {
        private readonly PortfolioContext _context;  //DI
        public _DefaultHeroComponentPartial(PortfolioContext context)
        {
            _context = context;
        }
        /// <summary>
        /// ////////////////////////////////
        /// </summary>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var hero=await _context.Heroes.AsNoTracking().FirstOrDefaultAsync(h => h.IsActive);
            return View(hero);
        }
    }
}
