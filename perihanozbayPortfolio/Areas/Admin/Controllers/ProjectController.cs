using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using perihanozbayPortfolio.Context;
using perihanozbayPortfolio.Entities;
namespace perihanozbayPortfolio.Areas.Admin.Controllers {
[Area("Admin")]
public class ProjectController : Controller {
 private readonly PortfolioContext _db;
 public ProjectController(PortfolioContext db) => _db=db;
 private async Task LoadOptions(int? selected = null) { ViewBag.Options = new SelectList(await _db.ProjectCategories.AsNoTracking().OrderBy(x => x.Name).ToListAsync(), "ProjectCategoryId", "Name", selected); }
 public async Task<IActionResult> Index() {
 var items=await _db.Projects .Include(x => x.ProjectCategory).AsNoTracking().ToListAsync();
 return View(items);
 }
 [HttpGet] public async Task<IActionResult> Create() { await LoadOptions(); return View(new Project()); }
 [HttpPost, ValidateAntiForgeryToken]
 public async Task<IActionResult> Create(Project model) {
 ModelState.Remove(nameof(Project.ProjectCategory)); if(!await _db.ProjectCategories.AnyAsync(x => x.ProjectCategoryId == model.ProjectCategoryId)) ModelState.AddModelError("ProjectCategoryId","Geçerli bir kategori/üst kayıt seçin.");
 if(!ModelState.IsValid) { await LoadOptions(model.ProjectCategoryId); return View(model); }
 _db.Set<Project>().Add(model); await _db.SaveChangesAsync();
 TempData["Success"]="Kayıt eklendi.";return RedirectToAction(nameof(Index));
 }
 [HttpGet] public async Task<IActionResult> Edit(int id) {
 var item=await _db.Set<Project>().FindAsync(id);
 if(item==null)return NotFound(); await LoadOptions(item.ProjectCategoryId); return View(item);
 }
 [HttpPost, ValidateAntiForgeryToken, ActionName("Edit")]
 public async Task<IActionResult> SaveEdit(int id) {
 var item=await _db.Set<Project>().FindAsync(id);if(item==null)return NotFound();
 if(await TryUpdateModelAsync(item,"",x => x.Title,x => x.Description,x => x.Detail,x => x.ImageUrl,x => x.GithubUrl,x => x.DemoUrl,x => x.IsActive,x => x.IsFeatured,x => x.Status,x => x.Technologies,x => x.Features,x => x.DisplayOrder,x => x.IsWide,x => x.CodeFileName,x => x.CodeLanguage,x => x.CodeSnippet,x => x.ProjectCategoryId)) {
 if(!await _db.ProjectCategories.AnyAsync(x => x.ProjectCategoryId == item.ProjectCategoryId)) ModelState.AddModelError("ProjectCategoryId","Geçerli bir üst kayıt seçin.");
 if(ModelState.IsValid) { await _db.SaveChangesAsync(); TempData["Success"]="Kayıt güncellendi.";return RedirectToAction(nameof(Index)); }
 }
 await LoadOptions(item.ProjectCategoryId); return View(item);
 }
 [HttpPost,ValidateAntiForgeryToken]
 public async Task<IActionResult> Delete(int id) {
 var item=await _db.Set<Project>().FindAsync(id);if(item==null)return NotFound();

 _db.Set<Project>().Remove(item);await _db.SaveChangesAsync();
 TempData["Success"]="Kayıt silindi.";return RedirectToAction(nameof(Index));
 }
}
}
