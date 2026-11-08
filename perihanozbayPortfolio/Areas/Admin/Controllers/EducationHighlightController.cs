using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using perihanozbayPortfolio.Context;
using perihanozbayPortfolio.Entities;
namespace perihanozbayPortfolio.Areas.Admin.Controllers {
[Area("Admin")]
public class EducationHighlightController : Controller {
 private readonly PortfolioContext _db;
 public EducationHighlightController(PortfolioContext db) => _db=db;
 private async Task LoadOptions(int? selected = null) { ViewBag.Options = new SelectList(await _db.Educations.AsNoTracking().OrderBy(x => x.Title).ToListAsync(), "EducationId", "Title", selected); }
 public async Task<IActionResult> Index() {
 var items=await _db.EducationHighlights .Include(x => x.Education).AsNoTracking().ToListAsync();
 return View(items);
 }
 [HttpGet] public async Task<IActionResult> Create() { await LoadOptions(); return View(new EducationHighlight()); }
 [HttpPost, ValidateAntiForgeryToken]
 public async Task<IActionResult> Create(EducationHighlight model) {
 ModelState.Remove(nameof(EducationHighlight.Education)); if(!await _db.Educations.AnyAsync(x => x.EducationId == model.EducationId)) ModelState.AddModelError("EducationId","Geçerli bir kategori/üst kayıt seçin.");
 if(!ModelState.IsValid) { await LoadOptions(model.EducationId); return View(model); }
 _db.Set<EducationHighlight>().Add(model); await _db.SaveChangesAsync();
 TempData["Success"]="Kayıt eklendi.";return RedirectToAction(nameof(Index));
 }
 [HttpGet] public async Task<IActionResult> Edit(int id) {
 var item=await _db.Set<EducationHighlight>().FindAsync(id);
 if(item==null)return NotFound(); await LoadOptions(item.EducationId); return View(item);
 }
 [HttpPost, ValidateAntiForgeryToken, ActionName("Edit")]
 public async Task<IActionResult> SaveEdit(int id) {
 var item=await _db.Set<EducationHighlight>().FindAsync(id);if(item==null)return NotFound();
 if(await TryUpdateModelAsync(item,"",x => x.Name,x => x.DisplayOrder,x => x.EducationId)) {
 if(!await _db.Educations.AnyAsync(x => x.EducationId == item.EducationId)) ModelState.AddModelError("EducationId","Geçerli bir üst kayıt seçin.");
 if(ModelState.IsValid) { await _db.SaveChangesAsync(); TempData["Success"]="Kayıt güncellendi.";return RedirectToAction(nameof(Index)); }
 }
 await LoadOptions(item.EducationId); return View(item);
 }
 [HttpPost,ValidateAntiForgeryToken]
 public async Task<IActionResult> Delete(int id) {
 var item=await _db.Set<EducationHighlight>().FindAsync(id);if(item==null)return NotFound();

 _db.Set<EducationHighlight>().Remove(item);await _db.SaveChangesAsync();
 TempData["Success"]="Kayıt silindi.";return RedirectToAction(nameof(Index));
 }
}
}
