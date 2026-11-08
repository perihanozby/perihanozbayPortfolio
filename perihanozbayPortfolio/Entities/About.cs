namespace perihanozbayPortfolio.Entities
{
    public class About
    {
        public int AboutId { get; set; }

        public string FullName { get; set; } = null!;
        public string Title { get; set; } = null!;

        public string ShortDescription { get; set; } = null!;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        public string? EngineeringPrinciple { get; set; }

        public string? Degree { get; set; }
        public string? University { get; set; }
        public string? MainFocus { get; set; }
        public string? Location { get; set; }
        public string? Languages { get; set; }

        public string? CareerStatusTitle { get; set; }
        public string? CareerStatusDescription { get; set; }
    }
}