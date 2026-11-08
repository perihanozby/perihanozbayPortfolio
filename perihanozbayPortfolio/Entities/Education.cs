
using System.ComponentModel.DataAnnotations;

namespace perihanozbayPortfolio.Entities
{
    public class Education
    {
        public int EducationId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Institution { get; set; }

        [MaxLength(200)]
        public string? Subtitle { get; set; }

        [MaxLength(100)]
        public string? Period { get; set; }

        [MaxLength(100)]
        public string? Badge { get; set; }

        [MaxLength(2000)]
        public string? Description { get; set; }

        public bool IsCurrent { get; set; } = false;

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<EducationHighlight> Highlights { get; set; }
            = new List<EducationHighlight>();
    }
}
