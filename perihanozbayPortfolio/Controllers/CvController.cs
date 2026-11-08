using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using perihanozbayPortfolio.Context;

namespace perihanozbayPortfolio.Controllers
{
    public class CvController : Controller
    {
        private readonly PortfolioContext _db;
        private readonly IWebHostEnvironment _environment;
        public CvController(PortfolioContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Download()
        {
            var fileName = await _db.SiteSettings.AsNoTracking()
                .OrderBy(x => x.SiteSettingId)
                .Select(x => x.CvFileUrl)
                .FirstOrDefaultAsync();
            if (string.IsNullOrWhiteSpace(fileName) ||
                !Guid.TryParse(Path.GetFileNameWithoutExtension(fileName), out _) ||
                !string.Equals(Path.GetExtension(fileName), ".pdf", StringComparison.OrdinalIgnoreCase))
                return NotFound("CV henüz yüklenmedi.");

            var path = Path.Combine(_environment.ContentRootPath, "App_Data", "cv", fileName);
            if (!System.IO.File.Exists(path)) return NotFound("CV dosyası bulunamadı.");
            return PhysicalFile(path, "application/pdf", "Perihan_Ozbay_CV.pdf");
        }
    }
}
