using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using perihanozbayPortfolio.Context;
using perihanozbayPortfolio.Entities;
namespace perihanozbayPortfolio.Areas.Admin.Controllers {
[Area("Admin")]
public class SkillController : Controller {
 private readonly PortfolioContext _db;
 public SkillController(PortfolioContext db) => _db=db;
 private async Task LoadOptions(int? selected = null) { ViewBag.Options = new SelectList(await _db.SkillCategories.AsNoTracking().OrderBy(x => x.Name).ToListAsync(), "SkillCategoryId", "Name", selected); }
 public async Task<IActionResult> Index() {
 var items=await _db.Skills .Include(x => x.SkillCategory).AsNoTracking().ToListAsync();
 return View(items);
 }
 [HttpGet] public async Task<IActionResult> Create() { await LoadOptions(); return View(new Skill()); }
 [HttpPost, ValidateAntiForgeryToken]
 public async Task<IActionResult> Create(Skill model) {
 ModelState.Remove(nameof(Skill.SkillCategory)); if(!await _db.SkillCategories.AnyAsync(x => x.SkillCategoryId == model.SkillCategoryId)) ModelState.AddModelError("SkillCategoryId","Geçerli bir kategori/üst kayıt seçin.");
 if(!ModelState.IsValid) { await LoadOptions(model.SkillCategoryId); return View(model); }
 _db.Set<Skill>().Add(model); await _db.SaveChangesAsync();
 TempData["Success"]="Kayıt eklendi.";return RedirectToAction(nameof(Index));
 }
 [HttpGet] public async Task<IActionResult> Edit(int id) {
 var item=await _db.Set<Skill>().FindAsync(id);
 if(item==null)return NotFound(); await LoadOptions(item.SkillCategoryId); return View(item);
 }
 [HttpPost, ValidateAntiForgeryToken, ActionName("Edit")]
 public async Task<IActionResult> SaveEdit(int id) {
 var item=await _db.Set<Skill>().FindAsync(id);if(item==null)return NotFound();
 if(await TryUpdateModelAsync(item,"",x => x.Name,x => x.Percentage,x => x.Description,x => x.DisplayOrder,x => x.IsActive,x => x.SkillCategoryId)) {
 if(!await _db.SkillCategories.AnyAsync(x => x.SkillCategoryId == item.SkillCategoryId)) ModelState.AddModelError("SkillCategoryId","Geçerli bir üst kayıt seçin.");
 if(ModelState.IsValid) { await _db.SaveChangesAsync(); TempData["Success"]="Kayıt güncellendi.";return RedirectToAction(nameof(Index)); }
 }
 await LoadOptions(item.SkillCategoryId); return View(item);
 }
 [HttpPost,ValidateAntiForgeryToken]
 public async Task<IActionResult> Delete(int id) {
 var item=await _db.Set<Skill>().FindAsync(id);if(item==null)return NotFound();

 _db.Set<Skill>().Remove(item);await _db.SaveChangesAsync();
 TempData["Success"]="Kayıt silindi.";return RedirectToAction(nameof(Index));
 }
}
}
