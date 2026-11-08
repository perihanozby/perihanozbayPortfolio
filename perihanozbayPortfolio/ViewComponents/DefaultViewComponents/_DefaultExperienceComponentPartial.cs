
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using perihanozbayPortfolio.Context;

namespace perihanozbayPortfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultExperienceComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultExperienceComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var experiences = await _context.Experiences
                .AsNoTracking()
                .Where(x => x.IsActive)
                .Include(x => x.Highlights)
                .OrderBy(x => x.DisplayOrder)
                .ThenBy(x => x.ExperienceId)
                .ToListAsync();

            foreach (var experience in experiences)
            {
                experience.Highlights = experience.Highlights
                    .OrderBy(x => x.DisplayOrder)
                    .ThenBy(x => x.ExperienceHighlightId)
                    .ToList();
            }

            return View(experiences);
        }
    }
}
