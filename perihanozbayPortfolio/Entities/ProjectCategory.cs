namespace perihanozbayPortfolio.Entities
{
    public class ProjectCategory
    {
        public int ProjectCategoryId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Project> Projects { get; set; } = new List<Project>();


    }
}
