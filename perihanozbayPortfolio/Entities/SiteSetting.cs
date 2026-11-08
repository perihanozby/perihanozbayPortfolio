
using System.ComponentModel.DataAnnotations;

namespace perihanozbayPortfolio.Entities
{
    public class SiteSetting
    {
        public int SiteSettingId { get; set; }

        [Required]
        [MaxLength(150)]
        public string SiteName { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? LogoText { get; set; }

        [MaxLength(500)]
        public string? LogoUrl { get; set; }

        [MaxLength(1000)]
        public string? FooterDescription { get; set; }

        [MaxLength(500)]
        public string? CvFileUrl { get; set; }

        [MaxLength(200)]
        public string? CopyrightText { get; set; }

        [MaxLength(200)]
        public string? FooterCredit { get; set; }
    }
}
