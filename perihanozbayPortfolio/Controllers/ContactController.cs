
using Microsoft.AspNetCore.Mvc;
using perihanozbayPortfolio.Context;
using perihanozbayPortfolio.Entities;

namespace perihanozbayPortfolio.Controllers
{
    public class ContactController : Controller
    {
        private readonly PortfolioContext _context;

        public ContactController(PortfolioContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(
            [FromForm] Message message)
        {
            // Ziyaretçinin belirleyemeyeceği alanlar
            ModelState.Remove(nameof(Message.MessageId));
            ModelState.Remove(nameof(Message.CreatedAt));
            ModelState.Remove(nameof(Message.IsRead));

            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value != null &&
                                x.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value!.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToList();

                return BadRequest(new
                {
                    success = false,
                    message = "Lütfen form alanlarını kontrol edin.",
                    errors
                });
            }

            var newMessage = new Message
            {
                FullName = message.FullName.Trim(),
                Email = message.Email.Trim(),
                Subject = message.Subject.Trim(),
                Content = message.Content.Trim(),
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };

            try
            {
                _context.Messages.Add(newMessage);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    message = "Mesajınız başarıyla kaydedildi."
                });
            }
            catch
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Mesaj kaydedilemedi. Lütfen daha sonra tekrar deneyin."
                });
            }
        }
    }
}
