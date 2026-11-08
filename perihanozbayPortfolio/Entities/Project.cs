
using System.ComponentModel.DataAnnotations;

namespace perihanozbayPortfolio.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public string? Detail { get; set; }

        public string? ImageUrl { get; set; }

        public string? GithubUrl { get; set; }

        public string? DemoUrl { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsFeatured { get; set; }

        // Proje durumu
        [MaxLength(100)]
        public string? Status { get; set; }

        // Teknolojiler - her satır bir teknoloji
        public string? Technologies { get; set; }

        // Teknik özellikler - her satır bir özellik
        public string? Features { get; set; }

        // Projelerin sıralaması
        public int DisplayOrder { get; set; }

        // Büyük kart görünümü
        public bool IsWide { get; set; }

        // Proje görselindeki kod paneli
        [MaxLength(200)]
        public string? CodeFileName { get; set; }

        [MaxLength(100)]
        public string? CodeLanguage { get; set; }

        public string? CodeSnippet { get; set; }

        // Kategori ilişkisi
        public int ProjectCategoryId { get; set; }

        public ProjectCategory ProjectCategory { get; set; } = null!;
    }
}
