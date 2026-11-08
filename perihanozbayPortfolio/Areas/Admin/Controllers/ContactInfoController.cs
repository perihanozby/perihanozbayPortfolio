using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using perihanozbayPortfolio.Context;
using perihanozbayPortfolio.Entities;
namespace perihanozbayPortfolio.Areas.Admin.Controllers {
[Area("Admin")]
public class ContactInfoController : Controller {
 private readonly PortfolioContext _db;
 public ContactInfoController(PortfolioContext db) => _db=db;

 public async Task<IActionResult> Index() {
 var items=await _db.ContactInfos .AsNoTracking().ToListAsync();
 return View(items);
 }
 [HttpGet] public async Task<IActionResult> Create() {  return View(new ContactInfo()); }
 [HttpPost, ValidateAntiForgeryToken]
 public async Task<IActionResult> Create(ContactInfo model) {

 if(!ModelState.IsValid) {  return View(model); }
 _db.Set<ContactInfo>().Add(model); await _db.SaveChangesAsync();
 TempData["Success"]="Kayıt eklendi.";return RedirectToAction(nameof(Index));
 }
 [HttpGet] public async Task<IActionResult> Edit(int id) {
 var item=await _db.Set<ContactInfo>().FindAsync(id);
 if(item==null)return NotFound();  return View(item);
 }
 [HttpPost, ValidateAntiForgeryToken, ActionName("Edit")]
 public async Task<IActionResult> SaveEdit(int id) {
 var item=await _db.Set<ContactInfo>().FindAsync(id);if(item==null)return NotFound();
 if(await TryUpdateModelAsync(item,"",x => x.Email,x => x.LinkedInUrl,x => x.GithubUrl,x => x.Location,x => x.WorkModel,x => x.Title,x => x.Description,x => x.IsAvailable)) {

 if(ModelState.IsValid) { await _db.SaveChangesAsync(); TempData["Success"]="Kayıt güncellendi.";return RedirectToAction(nameof(Index)); }
 }
  return View(item);
 }
 [HttpPost,ValidateAntiForgeryToken]
 public async Task<IActionResult> Delete(int id) {
 var item=await _db.Set<ContactInfo>().FindAsync(id);if(item==null)return NotFound();

 _db.Set<ContactInfo>().Remove(item);await _db.SaveChangesAsync();
 TempData["Success"]="Kayıt silindi.";return RedirectToAction(nameof(Index));
 }
}
}
