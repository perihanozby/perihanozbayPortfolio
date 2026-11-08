using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using perihanozbayPortfolio.Context;
using perihanozbayPortfolio.Entities;
namespace perihanozbayPortfolio.Areas.Admin.Controllers {
[Area("Admin")]
public class ExperienceHighlightController : Controller {
 private readonly PortfolioContext _db;
 public ExperienceHighlightController(PortfolioContext db) => _db=db;
 private async Task LoadOptions(int? selected = null) { ViewBag.Options = new SelectList(await _db.Experiences.AsNoTracking().OrderBy(x => x.Title).ToListAsync(), "ExperienceId", "Title", selected); }
 public async Task<IActionResult> Index() {
 var items=await _db.ExperienceHighlights .Include(x => x.Experience).AsNoTracking().ToListAsync();
 return View(items);
 }
 [HttpGet] public async Task<IActionResult> Create() { await LoadOptions(); return View(new ExperienceHighlight()); }
 [HttpPost, ValidateAntiForgeryToken]
 public async Task<IActionResult> Create(ExperienceHighlight model) {
 ModelState.Remove(nameof(ExperienceHighlight.Experience)); if(!await _db.Experiences.AnyAsync(x => x.ExperienceId == model.ExperienceId)) ModelState.AddModelError("ExperienceId","Geçerli bir kategori/üst kayıt seçin.");
 if(!ModelState.IsValid) { await LoadOptions(model.ExperienceId); return View(model); }
 _db.Set<ExperienceHighlight>().Add(model); await _db.SaveChangesAsync();
 TempData["Success"]="Kayıt eklendi.";return RedirectToAction(nameof(Index));
 }
 [HttpGet] public async Task<IActionResult> Edit(int id) {
 var item=await _db.Set<ExperienceHighlight>().FindAsync(id);
 if(item==null)return NotFound(); await LoadOptions(item.ExperienceId); return View(item);
 }
 [HttpPost, ValidateAntiForgeryToken, ActionName("Edit")]
 public async Task<IActionResult> SaveEdit(int id) {
 var item=await _db.Set<ExperienceHighlight>().FindAsync(id);if(item==null)return NotFound();
 if(await TryUpdateModelAsync(item,"",x => x.Description,x => x.DisplayOrder,x => x.ExperienceId)) {
 if(!await _db.Experiences.AnyAsync(x => x.ExperienceId == item.ExperienceId)) ModelState.AddModelError("ExperienceId","Geçerli bir üst kayıt seçin.");
 if(ModelState.IsValid) { await _db.SaveChangesAsync(); TempData["Success"]="Kayıt güncellendi.";return RedirectToAction(nameof(Index)); }
 }
 await LoadOptions(item.ExperienceId); return View(item);
 }
 [HttpPost,ValidateAntiForgeryToken]
 public async Task<IActionResult> Delete(int id) {
 var item=await _db.Set<ExperienceHighlight>().FindAsync(id);if(item==null)return NotFound();

 _db.Set<ExperienceHighlight>().Remove(item);await _db.SaveChangesAsync();
 TempData["Success"]="Kayıt silindi.";return RedirectToAction(nameof(Index));
 }
}
}
