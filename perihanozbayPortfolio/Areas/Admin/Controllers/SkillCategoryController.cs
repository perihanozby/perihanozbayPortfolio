using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using perihanozbayPortfolio.Context;
using perihanozbayPortfolio.Entities;
namespace perihanozbayPortfolio.Areas.Admin.Controllers {
[Area("Admin")]
public class SkillCategoryController : Controller {
 private readonly PortfolioContext _db;
 public SkillCategoryController(PortfolioContext db) => _db=db;

 public async Task<IActionResult> Index() {
 var items=await _db.SkillCategories .AsNoTracking().ToListAsync();
 return View(items);
 }
 [HttpGet] public async Task<IActionResult> Create() {  return View(new SkillCategory()); }
 [HttpPost, ValidateAntiForgeryToken]
 public async Task<IActionResult> Create(SkillCategory model) {

 if(!ModelState.IsValid) {  return View(model); }
 _db.Set<SkillCategory>().Add(model); await _db.SaveChangesAsync();
 TempData["Success"]="Kayıt eklendi.";return RedirectToAction(nameof(Index));
 }
 [HttpGet] public async Task<IActionResult> Edit(int id) {
 var item=await _db.Set<SkillCategory>().FindAsync(id);
 if(item==null)return NotFound();  return View(item);
 }
 [HttpPost, ValidateAntiForgeryToken, ActionName("Edit")]
 public async Task<IActionResult> SaveEdit(int id) {
 var item=await _db.Set<SkillCategory>().FindAsync(id);if(item==null)return NotFound();
 if(await TryUpdateModelAsync(item,"",x => x.Name,x => x.Icon,x => x.Badge,x => x.ProgressBarColor,x => x.IsWide,x => x.DisplayOrder,x => x.IsActive)) {

 if(ModelState.IsValid) { await _db.SaveChangesAsync(); TempData["Success"]="Kayıt güncellendi.";return RedirectToAction(nameof(Index)); }
 }
  return View(item);
 }
 [HttpPost,ValidateAntiForgeryToken]
 public async Task<IActionResult> Delete(int id) {
 var item=await _db.Set<SkillCategory>().FindAsync(id);if(item==null)return NotFound();
 if(await _db.Entry(item).Collection(x => x.Skills).Query().AnyAsync()) { TempData["Success"]="Bu kayda bağlı alt kayıtlar var; önce onları silin."; return RedirectToAction(nameof(Index)); }
 _db.Set<SkillCategory>().Remove(item);await _db.SaveChangesAsync();
 TempData["Success"]="Kayıt silindi.";return RedirectToAction(nameof(Index));
 }
}
}
