
using System.ComponentModel.DataAnnotations;

namespace perihanozbayPortfolio.Entities
{
    public class Hero
    {
        public int HeroId { get; set; }

        [Required]
        [MaxLength(150)]
        public string BadgeText { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string GreetingText { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string HighlightedName { get; set; } = string.Empty;

        [Required]
        [MaxLength(300)]
        public string Headline { get; set; } = string.Empty;

        [MaxLength(1500)]
        public string? Description { get; set; }

        [MaxLength(500)]
        public string? ImageUrl { get; set; }

        [MaxLength(150)]
        public string? EducationBadge { get; set; }

        [MaxLength(150)]
        public string? AvailabilityBadge { get; set; }

        [MaxLength(100)]
        public string? Expertise { get; set; }

        [MaxLength(100)]
        public string? Approach { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
