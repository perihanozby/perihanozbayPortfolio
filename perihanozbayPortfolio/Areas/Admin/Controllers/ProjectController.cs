
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using perihanozbayPortfolio.Context;
using perihanozbayPortfolio.Entities;

namespace perihanozbayPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectController : Controller
    {
        private readonly PortfolioContext _db;
        private readonly IWebHostEnvironment _environment;

        private const long MaxImageSize = 5 * 1024 * 1024;

        public ProjectController(
            PortfolioContext db,
            IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }

        private async Task LoadOptions(int? selected = null)
        {
            ViewBag.Options = new SelectList(
                await _db.ProjectCategories
                    .AsNoTracking()
                    .OrderBy(x => x.Name)
                    .ToListAsync(),
                "ProjectCategoryId",
                "Name",
                selected
            );
        }

        public async Task<IActionResult> Index()
        {
            var items = await _db.Projects
                .Include(x => x.ProjectCategory)
                .AsNoTracking()
                .ToListAsync();

            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadOptions();
            return View(new Project());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            Project model,
            IFormFile? imageFile)
        {
            ModelState.Remove(nameof(Project.ProjectCategory));

            if (!await _db.ProjectCategories.AnyAsync(
                x => x.ProjectCategoryId == model.ProjectCategoryId))
            {
                ModelState.AddModelError(
                    nameof(Project.ProjectCategoryId),
                    "Geçerli bir kategori seçin.");
            }

            ValidateImage(imageFile);

            if (!ModelState.IsValid)
            {
                await LoadOptions(model.ProjectCategoryId);
                return View(model);
            }

            if (imageFile != null && imageFile.Length > 0)
            {
                model.ImageUrl = await SaveImageAsync(imageFile);
            }

            _db.Projects.Add(model);
            await _db.SaveChangesAsync();

            TempData["Success"] = "Proje başarıyla eklendi.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _db.Projects.FindAsync(id);

            if (item == null)
                return NotFound();

            await LoadOptions(item.ProjectCategoryId);

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public async Task<IActionResult> SaveEdit(
            int id,
            IFormFile? imageFile)
        {
            var item = await _db.Projects.FindAsync(id);

            if (item == null)
                return NotFound();

            ValidateImage(imageFile);

            var updated = await TryUpdateModelAsync(
                item,
                "",
                x => x.Title,
                x => x.Description,
                x => x.Detail,
                x => x.ImageUrl,
                x => x.GithubUrl,
                x => x.DemoUrl,
                x => x.IsActive,
                x => x.IsFeatured,
                x => x.Status,
                x => x.Technologies,
                x => x.Features,
                x => x.DisplayOrder,
                x => x.IsWide,
                x => x.CodeFileName,
                x => x.CodeLanguage,
                x => x.CodeSnippet,
                x => x.ProjectCategoryId
            );

            if (!await _db.ProjectCategories.AnyAsync(
                x => x.ProjectCategoryId == item.ProjectCategoryId))
            {
                ModelState.AddModelError(
                    nameof(Project.ProjectCategoryId),
                    "Geçerli bir kategori seçin.");
            }

            if (updated && ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    item.ImageUrl = await SaveImageAsync(imageFile);
                }

                await _db.SaveChangesAsync();

                TempData["Success"] = "Proje başarıyla güncellendi.";

                return RedirectToAction(nameof(Index));
            }

            await LoadOptions(item.ProjectCategoryId);

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _db.Projects.FindAsync(id);

            if (item == null)
                return NotFound();

            _db.Projects.Remove(item);
            await _db.SaveChangesAsync();

            TempData["Success"] = "Proje silindi.";

            return RedirectToAction(nameof(Index));
        }

        private void ValidateImage(IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return;

            if (file.Length > MaxImageSize)
            {
                ModelState.AddModelError(
                    "imageFile",
                    "Görsel en fazla 5 MB olabilir.");

                return;
            }

            var extension = Path.GetExtension(
                file.FileName).ToLowerInvariant();

            if (extension != ".jpg" &&
                extension != ".jpeg" &&
                extension != ".png" &&
                extension != ".webp")
            {
                ModelState.AddModelError(
                    "imageFile",
                    "Yalnızca JPG, PNG ve WebP dosyaları yüklenebilir.");

                return;
            }

            using var stream = file.OpenReadStream();

            Span<byte> header = stackalloc byte[12];

            var bytesRead = stream.Read(header);

            var valid = extension switch
            {
                ".jpg" or ".jpeg" =>
                    bytesRead >= 3 &&
                    header[0] == 0xFF &&
                    header[1] == 0xD8 &&
                    header[2] == 0xFF,

                ".png" =>
                    bytesRead >= 8 &&
                    header[..8].SequenceEqual(
                        new byte[]
                        {
                            137, 80, 78, 71, 13, 10, 26, 10
                        }),

                ".webp" =>
                    bytesRead >= 12 &&
                    header[..4].SequenceEqual(
                        "RIFF"u8) &&
                    header[8..12].SequenceEqual(
                        "WEBP"u8),

                _ => false
            };

            if (!valid)
            {
                ModelState.AddModelError(
                    "imageFile",
                    "Dosyanın gerçek biçimi seçilen görsel türüyle uyuşmuyor.");
            }
        }

        private async Task<string> SaveImageAsync(
            IFormFile file)
        {
            var folder = Path.Combine(
                _environment.WebRootPath,
                "uploads",
                "projects");

            Directory.CreateDirectory(folder);

            var extension = Path.GetExtension(
                file.FileName).ToLowerInvariant();

            var fileName =
                $"{Guid.NewGuid():N}{extension}";

            var fullPath = Path.Combine(folder, fileName);

            await using var stream = new FileStream(
                fullPath,
                FileMode.CreateNew);

            await file.CopyToAsync(stream);

            return $"/uploads/projects/{fileName}";
        }
    }
}
