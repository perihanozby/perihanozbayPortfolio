
using System.ComponentModel.DataAnnotations;

namespace perihanozbayPortfolio.Entities
{
    public class SkillCategory
    {
        public int SkillCategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Icon { get; set; }

        [MaxLength(100)]
        public string? Badge { get; set; }

        [MaxLength(100)]
        public string? ProgressBarColor { get; set; }

        public bool IsWide { get; set; } = false;

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Skill> Skills { get; set; }
            = new List<Skill>();

        public ICollection<SkillTag> SkillTags { get; set; }
            = new List<SkillTag>();
    }
}
