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

    }
}
