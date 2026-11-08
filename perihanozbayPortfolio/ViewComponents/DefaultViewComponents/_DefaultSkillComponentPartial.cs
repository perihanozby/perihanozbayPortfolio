
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using perihanozbayPortfolio.Context;

namespace perihanozbayPortfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultSkillComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultSkillComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _context.SkillCategories
                .AsNoTracking()
                .Where(x => x.IsActive)
                .Include(x => x.Skills.Where(s => s.IsActive))
                .Include(x => x.SkillTags.Where(t => t.IsActive))
                .AsSplitQuery()
                .OrderBy(x => x.DisplayOrder)
                .ThenBy(x => x.SkillCategoryId)
                .ToListAsync();

            foreach (var category in categories)
            {
                category.Skills = category.Skills
                    .OrderBy(x => x.DisplayOrder)
                    .ThenBy(x => x.SkillId)
                    .ToList();

                category.SkillTags = category.SkillTags
                    .OrderBy(x => x.DisplayOrder)
                    .ThenBy(x => x.SkillTagId)
                    .ToList();
            }

            return View(categories);
        }
    }
}
