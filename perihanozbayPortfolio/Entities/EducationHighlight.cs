
using System.ComponentModel.DataAnnotations;

namespace perihanozbayPortfolio.Entities
{
    public class EducationHighlight
    {
        public int EducationHighlightId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }

        public int EducationId { get; set; }

        public Education Education { get; set; } = null!;
    }
}
