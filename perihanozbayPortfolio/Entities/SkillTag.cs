
using System.ComponentModel.DataAnnotations;

namespace perihanozbayPortfolio.Entities
{
    public class SkillTag
    {
        public int SkillTagId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; } = true;

        public int SkillCategoryId { get; set; }

        public SkillCategory SkillCategory { get; set; } = null!;
    }
}
