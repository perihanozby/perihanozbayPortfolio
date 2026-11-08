
using System.ComponentModel.DataAnnotations;

namespace perihanozbayPortfolio.Entities
{
    public class ContactInfo
    {
        public int ContactInfoId { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(254)]
        public string Email { get; set; } = string.Empty;

        [Url]
        [StringLength(500)]
        public string? LinkedInUrl { get; set; }

        [Url]
        [StringLength(500)]
        public string? GithubUrl { get; set; }

        [StringLength(200)]
        public string? Location { get; set; }

        [StringLength(300)]
        public string? WorkModel { get; set; }

        [StringLength(200)]
        public string? Title { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}
