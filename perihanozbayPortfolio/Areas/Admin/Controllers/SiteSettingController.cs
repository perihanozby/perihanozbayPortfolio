using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using perihanozbayPortfolio.Context;
using perihanozbayPortfolio.Entities;
namespace perihanozbayPortfolio.Areas.Admin.Controllers {
[Area("Admin")]
public class SiteSettingController : Controller {
 private readonly PortfolioContext _db;
 public SiteSettingController(PortfolioContext db) => _db=db;

 public async Task<IActionResult> Index() {
 var items=await _db.SiteSettings .AsNoTracking().ToListAsync();
 return View(items);
 }
 [HttpGet] public async Task<IActionResult> Create() {  return View(new SiteSetting()); }
 [HttpPost, ValidateAntiForgeryToken]
 public async Task<IActionResult> Create(SiteSetting model) {

 if(!ModelState.IsValid) {  return View(model); }
 _db.Set<SiteSetting>().Add(model); await _db.SaveChangesAsync();
 TempData["Success"]="Kayıt eklendi.";return RedirectToAction(nameof(Index));
 }
 [HttpGet] public async Task<IActionResult> Edit(int id) {
 var item=await _db.Set<SiteSetting>().FindAsync(id);
 if(item==null)return NotFound();  return View(item);
 }
 [HttpPost, ValidateAntiForgeryToken, ActionName("Edit")]
 public async Task<IActionResult> SaveEdit(int id) {
 var item=await _db.Set<SiteSetting>().FindAsync(id);if(item==null)return NotFound();
 if(await TryUpdateModelAsync(item,"",x => x.SiteName,x => x.LogoText,x => x.LogoUrl,x => x.FooterDescription,x => x.CvFileUrl,x => x.CopyrightText,x => x.FooterCredit)) {

 if(ModelState.IsValid) { await _db.SaveChangesAsync(); TempData["Success"]="Kayıt güncellendi.";return RedirectToAction(nameof(Index)); }
 }
  return View(item);
 }
 [HttpPost,ValidateAntiForgeryToken]
 public async Task<IActionResult> Delete(int id) {
 var item=await _db.Set<SiteSetting>().FindAsync(id);if(item==null)return NotFound();

 _db.Set<SiteSetting>().Remove(item);await _db.SaveChangesAsync();
 TempData["Success"]="Kayıt silindi.";return RedirectToAction(nameof(Index));
 }
}
}
