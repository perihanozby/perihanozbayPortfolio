using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using perihanozbayPortfolio.Context;
using perihanozbayPortfolio.Entities;

namespace perihanozbayPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CvController : Controller
    {
        private const long MaxPdfBytes = 5 * 1024 * 1024;
        private readonly PortfolioContext _db;
        private readonly IWebHostEnvironment _environment;
        public CvController(PortfolioContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.CvFileName = await _db.SiteSettings.AsNoTracking()
                .OrderBy(x => x.SiteSettingId).Select(x => x.CvFileUrl).FirstOrDefaultAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(6 * 1024 * 1024)]
        public async Task<IActionResult> Upload(IFormFile? pdf)
        {
            if (pdf == null || pdf.Length == 0 || pdf.Length > MaxPdfBytes ||
                !string.Equals(Path.GetExtension(pdf.FileName), ".pdf", StringComparison.OrdinalIgnoreCase))
            {
                TempData["CvError"] = "Yalnızca 5 MB veya daha küçük PDF dosyaları kabul edilir.";
                return RedirectToAction(nameof(Index));
            }

            var directory = Path.Combine(_environment.ContentRootPath, "App_Data", "cv");
            Directory.CreateDirectory(directory);
            var newName = Guid.NewGuid().ToString("N") + ".pdf";
            var newPath = Path.Combine(directory, newName);
            try
            {
                await using (var output = new FileStream(newPath, FileMode.CreateNew, FileAccess.Write))
                {
                    await pdf.CopyToAsync(output);
                }

                // Check the PDF signature, not just the extension or MIME type.
                var header = new byte[5];
                await using (var input = new FileStream(newPath, FileMode.Open, FileAccess.Read))
                {
                    if (await input.ReadAsync(header.AsMemory(0, 5)) != 5 ||
                        !header.SequenceEqual(new byte[] { 0x25, 0x50, 0x44, 0x46, 0x2D }))
                    {
                        System.IO.File.Delete(newPath);
                        TempData["CvError"] = "Dosya geçerli bir PDF imzası taşımıyor.";
                        return RedirectToAction(nameof(Index));
                    }
                }

                var setting = await _db.SiteSettings.OrderBy(x => x.SiteSettingId).FirstOrDefaultAsync();
                if (setting == null)
                {
                    setting = new SiteSetting { SiteName = "Perihan Özbay" };
                    _db.SiteSettings.Add(setting);
                }
                var previous = setting.CvFileUrl;
                setting.CvFileUrl = newName;
                await _db.SaveChangesAsync();
                if (!string.IsNullOrWhiteSpace(previous) &&
                    Guid.TryParse(Path.GetFileNameWithoutExtension(previous), out _) &&
                    string.Equals(Path.GetExtension(previous), ".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    var oldPath = Path.Combine(directory, previous);
                    try { if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath); }
                    catch (IOException) { /* Old file cleanup can be retried manually. */ }
                    catch (UnauthorizedAccessException) { /* Keep new CV available. */ }
                }
                TempData["Success"] = "CV başarıyla yüklendi ve indirmeye açıldı.";
            }
            catch
            {
                if (System.IO.File.Exists(newPath)) System.IO.File.Delete(newPath);
                TempData["CvError"] = "Dosya yüklenirken hata oluştu. Kayıt ve klasör izinlerini kontrol edin.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
