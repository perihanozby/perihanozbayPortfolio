
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using perihanozbayPortfolio.Context;

namespace perihanozbayPortfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultMetricComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultMetricComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var metrics = await _context.Metrics
                .AsNoTracking()
                .Where(x => x.IsActive)
                .OrderBy(x => x.DisplayOrder)
                .ThenBy(x => x.MetricId)
                .ToListAsync();

            return View(metrics);
        }
    }
}
