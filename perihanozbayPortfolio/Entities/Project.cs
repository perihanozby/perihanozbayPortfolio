namespace perihanozbayPortfolio.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Detail { get; set; }
        public string? ImageUrl { get; set; }
        public string? GithubUrl { get; set; }
        public string? DemoUrl { get; set; }
        public bool IsActive { get; set; }
        public bool IsFeatured { get; set; }
        public int ProjectCategoryId { get; set; }
        public ProjectCategory ProjectCategory { get; set; } = null!;

    }
}
