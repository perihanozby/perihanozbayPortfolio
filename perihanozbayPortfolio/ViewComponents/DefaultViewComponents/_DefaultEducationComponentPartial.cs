
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using perihanozbayPortfolio.Context;

namespace perihanozbayPortfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultEducationComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultEducationComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var educations = await _context.Educations
                .AsNoTracking()
                .Where(x => x.IsActive)
                .Include(x => x.Highlights)
                .OrderBy(x => x.DisplayOrder)
                .ThenBy(x => x.EducationId)
                .ToListAsync();

            foreach (var education in educations)
            {
                education.Highlights = education.Highlights
                    .OrderBy(x => x.DisplayOrder)
                    .ThenBy(x => x.EducationHighlightId)
                    .ToList();
            }

            return View(educations);
        }
    }
}
