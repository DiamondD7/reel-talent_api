namespace reel_talent_api.Domain.Models.DTO
{
    public class TalentDetailsDTO
    {
        public Guid TalentId { get; set; }
        public string? Bio { get; set; }
        public string? Ethnicity { get; set; }
        public string? NaturalAccent { get; set; }
        public string? EyeColor { get; set; }
        public string? HairColor { get; set; }
        public string? ShoeSize { get; set; }
        public string? ClothingSize { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public List<string> Skills { get; set; } = new();
        public List<string> Languages { get; set; } = new();
        public List<TalentExperienceDTO> Experiences { get; set; } = new();
    }
}
