namespace reel_talent_api.Domain.Models.DTO
{
    public class TalentExperienceDTO
    {
        public Guid Id { get; set; }
        public string Year { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Production { get; set; }
        public string? DirectorOrCompany { get; set; }
        public string? Url { get; set; }
    }
}
