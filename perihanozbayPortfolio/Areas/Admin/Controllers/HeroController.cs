using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using perihanozbayPortfolio.Context;
using perihanozbayPortfolio.Entities;
namespace perihanozbayPortfolio.Areas.Admin.Controllers {
[Area("Admin")]
public class HeroController : Controller {
 private readonly PortfolioContext _db;
 public HeroController(PortfolioContext db) => _db=db;

 public async Task<IActionResult> Index() {
 var items=await _db.Heroes .AsNoTracking().ToListAsync();
 return View(items);
 }
 [HttpGet] public async Task<IActionResult> Create() {  return View(new Hero()); }
 [HttpPost, ValidateAntiForgeryToken]
 public async Task<IActionResult> Create(Hero model) {

 if(!ModelState.IsValid) {  return View(model); }
 _db.Set<Hero>().Add(model); await _db.SaveChangesAsync();
 TempData["Success"]="Kayıt eklendi.";return RedirectToAction(nameof(Index));
 }
 [HttpGet] public async Task<IActionResult> Edit(int id) {
 var item=await _db.Set<Hero>().FindAsync(id);
 if(item==null)return NotFound();  return View(item);
 }
 [HttpPost, ValidateAntiForgeryToken, ActionName("Edit")]
 public async Task<IActionResult> SaveEdit(int id) {
 var item=await _db.Set<Hero>().FindAsync(id);if(item==null)return NotFound();
 if(await TryUpdateModelAsync(item,"",x => x.BadgeText,x => x.GreetingText,x => x.HighlightedName,x => x.Headline,x => x.Description,x => x.ImageUrl,x => x.EducationBadge,x => x.AvailabilityBadge,x => x.Expertise,x => x.Approach,x => x.IsActive)) {

 if(ModelState.IsValid) { await _db.SaveChangesAsync(); TempData["Success"]="Kayıt güncellendi.";return RedirectToAction(nameof(Index)); }
 }
  return View(item);
 }
 [HttpPost,ValidateAntiForgeryToken]
 public async Task<IActionResult> Delete(int id) {
 var item=await _db.Set<Hero>().FindAsync(id);if(item==null)return NotFound();

 _db.Set<Hero>().Remove(item);await _db.SaveChangesAsync();
 TempData["Success"]="Kayıt silindi.";return RedirectToAction(nameof(Index));
 }
}
}
