using Microsoft.EntityFrameworkCore;
using perihanozbayPortfolio.Entities;

namespace perihanozbayPortfolio.Context
{
    public class PortfolioContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=PerihanPortfolioDb;Integrated Security=true; trustServerCertificate=true;");
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectCategory> ProjectCategories { get; set; }
        public DbSet<About> Abouts  { get; set; }

        public DbSet<SkillCategory> SkillCategories { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public DbSet<SkillTag> SkillTags { get; set; }

        public DbSet<Experience> Experiences { get; set; }

        public DbSet<ExperienceHighlight> ExperienceHighlights { get; set; }

        public DbSet<Education> Educations { get; set; }

        public DbSet<EducationHighlight> EducationHighlights { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<ContactInfo> ContactInfos { get; set; }

        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Metric> Metrics { get; set; }
        public DbSet<SiteSetting> SiteSettings { get; set; }



    }
}
