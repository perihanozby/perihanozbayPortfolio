
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using perihanozbayPortfolio.Context;

namespace perihanozbayPortfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultContactComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultContactComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var contactInfo = await _context.ContactInfos
                .AsNoTracking()
                .OrderBy(x => x.ContactInfoId)
                .FirstOrDefaultAsync();

            return View(contactInfo);
        }
    }
}
