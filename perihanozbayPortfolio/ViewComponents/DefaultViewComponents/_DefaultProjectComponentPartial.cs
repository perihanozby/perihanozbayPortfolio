
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using perihanozbayPortfolio.Context;

namespace perihanozbayPortfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultProjectComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultProjectComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var projects = await _context.Projects
                .AsNoTracking()
                .Include(x => x.ProjectCategory)
                .Where(x => x.IsActive && x.ProjectCategory.IsActive)
                .OrderBy(x => x.DisplayOrder)
                .ThenBy(x => x.ProjectId)
                .Take(3)
                .ToListAsync();

            return View(projects);
        }
    }
}
