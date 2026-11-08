using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using perihanozbayPortfolio.Context;
using perihanozbayPortfolio.Entities;
namespace perihanozbayPortfolio.Areas.Admin.Controllers {
[Area("Admin")]
public class AboutController : Controller {
 private readonly PortfolioContext _db;
 public AboutController(PortfolioContext db) => _db=db;

 public async Task<IActionResult> Index() {
 var items=await _db.Abouts .AsNoTracking().ToListAsync();
 return View(items);
 }
 [HttpGet] public async Task<IActionResult> Create() {  return View(new About()); }
 [HttpPost, ValidateAntiForgeryToken]
 public async Task<IActionResult> Create(About model) {

 if(!ModelState.IsValid) {  return View(model); }
 _db.Set<About>().Add(model); await _db.SaveChangesAsync();
 TempData["Success"]="Kayıt eklendi.";return RedirectToAction(nameof(Index));
 }
 [HttpGet] public async Task<IActionResult> Edit(int id) {
 var item=await _db.Set<About>().FindAsync(id);
 if(item==null)return NotFound();  return View(item);
 }
 [HttpPost, ValidateAntiForgeryToken, ActionName("Edit")]
 public async Task<IActionResult> SaveEdit(int id) {
 var item=await _db.Set<About>().FindAsync(id);if(item==null)return NotFound();
 if(await TryUpdateModelAsync(item,"",x => x.FullName,x => x.Title,x => x.ShortDescription,x => x.Description,x => x.ImageUrl,x => x.EngineeringPrinciple,x => x.Degree,x => x.University,x => x.MainFocus,x => x.Location,x => x.Languages,x => x.CareerStatusTitle,x => x.CareerStatusDescription)) {

 if(ModelState.IsValid) { await _db.SaveChangesAsync(); TempData["Success"]="Kayıt güncellendi.";return RedirectToAction(nameof(Index)); }
 }
  return View(item);
 }
 [HttpPost,ValidateAntiForgeryToken]
 public async Task<IActionResult> Delete(int id) {
 var item=await _db.Set<About>().FindAsync(id);if(item==null)return NotFound();

 _db.Set<About>().Remove(item);await _db.SaveChangesAsync();
 TempData["Success"]="Kayıt silindi.";return RedirectToAction(nameof(Index));
 }
}
}
