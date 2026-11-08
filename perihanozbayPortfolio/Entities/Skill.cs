
using System.ComponentModel.DataAnnotations;

namespace perihanozbayPortfolio.Entities
{
    public class Skill
    {
        public int SkillId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [Range(0, 100)]
        public int Percentage { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; } = true;

        public int SkillCategoryId { get; set; }

        public SkillCategory SkillCategory { get; set; } = null!;
    }
}
