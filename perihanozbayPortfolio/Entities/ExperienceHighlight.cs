
using System.ComponentModel.DataAnnotations;

namespace perihanozbayPortfolio.Entities
{
    public class ExperienceHighlight
    {
        public int ExperienceHighlightId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }

        public int ExperienceId { get; set; }

        public Experience Experience { get; set; } = null!;
    }
}
