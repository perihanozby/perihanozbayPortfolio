using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using perihanozbayPortfolio.Context;
using perihanozbayPortfolio.Entities;
namespace perihanozbayPortfolio.Areas.Admin.Controllers {
[Area("Admin")]
public class ProjectCategoryController : Controller {
 private readonly PortfolioContext _db;
 public ProjectCategoryController(PortfolioContext db) => _db=db;

 public async Task<IActionResult> Index() {
 var items=await _db.ProjectCategories .AsNoTracking().ToListAsync();
 return View(items);
 }
 [HttpGet] public async Task<IActionResult> Create() {  return View(new ProjectCategory()); }
 [HttpPost, ValidateAntiForgeryToken]
 public async Task<IActionResult> Create(ProjectCategory model) {

 if(!ModelState.IsValid) {  return View(model); }
 _db.Set<ProjectCategory>().Add(model); await _db.SaveChangesAsync();
 TempData["Success"]="Kayıt eklendi.";return RedirectToAction(nameof(Index));
 }
 [HttpGet] public async Task<IActionResult> Edit(int id) {
 var item=await _db.Set<ProjectCategory>().FindAsync(id);
 if(item==null)return NotFound();  return View(item);
 }
 [HttpPost, ValidateAntiForgeryToken, ActionName("Edit")]
 public async Task<IActionResult> SaveEdit(int id) {
 var item=await _db.Set<ProjectCategory>().FindAsync(id);if(item==null)return NotFound();
 if(await TryUpdateModelAsync(item,"",x => x.Name,x => x.Description,x => x.IsActive)) {

 if(ModelState.IsValid) { await _db.SaveChangesAsync(); TempData["Success"]="Kayıt güncellendi.";return RedirectToAction(nameof(Index)); }
 }
  return View(item);
 }
 [HttpPost,ValidateAntiForgeryToken]
 public async Task<IActionResult> Delete(int id) {
 var item=await _db.Set<ProjectCategory>().FindAsync(id);if(item==null)return NotFound();
 if(await _db.Entry(item).Collection(x => x.Projects).Query().AnyAsync()) { TempData["Success"]="Bu kayda bağlı alt kayıtlar var; önce onları silin."; return RedirectToAction(nameof(Index)); }
 _db.Set<ProjectCategory>().Remove(item);await _db.SaveChangesAsync();
 TempData["Success"]="Kayıt silindi.";return RedirectToAction(nameof(Index));
 }
}
}
