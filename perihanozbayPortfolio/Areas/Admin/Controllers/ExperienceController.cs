using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using perihanozbayPortfolio.Context;
using perihanozbayPortfolio.Entities;
namespace perihanozbayPortfolio.Areas.Admin.Controllers {
[Area("Admin")]
public class ExperienceController : Controller {
 private readonly PortfolioContext _db;
 public ExperienceController(PortfolioContext db) => _db=db;

 public async Task<IActionResult> Index() {
 var items=await _db.Experiences .AsNoTracking().ToListAsync();
 return View(items);
 }
 [HttpGet] public async Task<IActionResult> Create() {  return View(new Experience()); }
 [HttpPost, ValidateAntiForgeryToken]
 public async Task<IActionResult> Create(Experience model) {

 if(!ModelState.IsValid) {  return View(model); }
 _db.Set<Experience>().Add(model); await _db.SaveChangesAsync();
 TempData["Success"]="Kayıt eklendi.";return RedirectToAction(nameof(Index));
 }
 [HttpGet] public async Task<IActionResult> Edit(int id) {
 var item=await _db.Set<Experience>().FindAsync(id);
 if(item==null)return NotFound();  return View(item);
 }
 [HttpPost, ValidateAntiForgeryToken, ActionName("Edit")]
 public async Task<IActionResult> SaveEdit(int id) {
 var item=await _db.Set<Experience>().FindAsync(id);if(item==null)return NotFound();
 if(await TryUpdateModelAsync(item,"",x => x.Title,x => x.Subtitle,x => x.Period,x => x.Badge,x => x.Description,x => x.IsCurrent,x => x.DisplayOrder,x => x.IsActive)) {

 if(ModelState.IsValid) { await _db.SaveChangesAsync(); TempData["Success"]="Kayıt güncellendi.";return RedirectToAction(nameof(Index)); }
 }
  return View(item);
 }
 [HttpPost,ValidateAntiForgeryToken]
 public async Task<IActionResult> Delete(int id) {
 var item=await _db.Set<Experience>().FindAsync(id);if(item==null)return NotFound();
 if(await _db.Entry(item).Collection(x => x.Highlights).Query().AnyAsync()) { TempData["Success"]="Bu kayda bağlı alt kayıtlar var; önce onları silin."; return RedirectToAction(nameof(Index)); }
 _db.Set<Experience>().Remove(item);await _db.SaveChangesAsync();
 TempData["Success"]="Kayıt silindi.";return RedirectToAction(nameof(Index));
 }
}
}
