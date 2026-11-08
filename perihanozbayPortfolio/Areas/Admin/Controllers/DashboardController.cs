using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using perihanozbayPortfolio.Context;
namespace perihanozbayPortfolio.Areas.Admin.Controllers {
[Area("Admin")]
public class DashboardController : Controller {
 private readonly PortfolioContext _db;
 public DashboardController(PortfolioContext db) => _db=db;
 public async Task<IActionResult> Index(){
 ViewBag.ProjectCount=await _db.Projects.CountAsync();ViewBag.ActiveProjects=await _db.Projects.CountAsync(x=>x.IsActive);
 ViewBag.SkillCount=await _db.Skills.CountAsync();ViewBag.ExperienceCount=await _db.Experiences.CountAsync();
 ViewBag.EducationCount=await _db.Educations.CountAsync();ViewBag.MessageCount=await _db.Messages.CountAsync();
 ViewBag.UnreadCount=await _db.Messages.CountAsync(x=>!x.IsRead);
 ViewBag.RecentMessages=await _db.Messages.AsNoTracking().OrderByDescending(x=>x.CreatedAt).Take(5).ToListAsync();
 ViewBag.RecentProjects=await _db.Projects.Include(x=>x.ProjectCategory).AsNoTracking().OrderByDescending(x=>x.ProjectId).Take(3).ToListAsync();
 ViewBag.Categories=await _db.ProjectCategories.AsNoTracking().Select(x=>new {x.Name,Count=x.Projects.Count()}).ToListAsync();
 return View();
 }
}}
