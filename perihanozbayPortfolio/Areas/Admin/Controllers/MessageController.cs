using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using perihanozbayPortfolio.Context;
namespace perihanozbayPortfolio.Areas.Admin.Controllers {
[Area("Admin")]
public class MessageController:Controller {
 private readonly PortfolioContext _db;
 public MessageController(PortfolioContext db)=>_db=db;
 public async Task<IActionResult> Index(string? q,string sort="desc") {
 var query=_db.Messages.AsNoTracking().AsQueryable();
 if(!string.IsNullOrWhiteSpace(q))query=query.Where(x=>x.FullName.Contains(q)||x.Subject.Contains(q));
 ViewBag.Query=q;ViewBag.Sort=sort;ViewBag.Unread=await _db.Messages.CountAsync(x=>!x.IsRead);
 return View(sort=="asc"?await query.OrderBy(x=>x.CreatedAt).ToListAsync():await query.OrderByDescending(x=>x.CreatedAt).ToListAsync());
 }
 public async Task<IActionResult> Details(int id){var item=await _db.Messages.FindAsync(id);if(item==null)return NotFound();if(!item.IsRead){item.IsRead=true;await _db.SaveChangesAsync();}return View(item);}
 [HttpPost,ValidateAntiForgeryToken]public async Task<IActionResult> Delete(int id){var item=await _db.Messages.FindAsync(id);if(item==null)return NotFound();_db.Messages.Remove(item);await _db.SaveChangesAsync();TempData["Success"]="Mesaj silindi.";return RedirectToAction(nameof(Index));}
}}
