using System.ComponentModel.DataAnnotations;

namespace reel_talent_api.Domain.Models
{

    public class Talent
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public TalentDetails? Details { get; set; } //one to one relationship
    }

    public class TalentDetails
    {
        [Key]
        public Guid TalentId { get; set; }
        public Talent Talent { get; set; } = null!;
        public string? Bio { get; set; }
        public string? Ethnicity { get; set; }
        public string? NaturalAccent { get; set; }
        public string? EyeColor { get; set; }
        public string? HairColor { get; set; }
        public string? ShoeSize { get; set; }
        public string? ClothingSize { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public ICollection<TalentSkill> Skills { get; set; } = new List<TalentSkill>();
        public ICollection<TalentLanguage> Languages { get; set; } = new List<TalentLanguage>();
        public ICollection<TalentExperience> Experiences { get; set; } = new List<TalentExperience>();
    }

    public class TalentSkill
    {
        public Guid Id { get; set; }
        public Guid TalentDetailsId { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class TalentLanguage
    {
        public Guid Id { get; set; }
        public Guid TalentDetailsId { get; set; }
        public string Name { get; set; } = string.Empty;
    
    }

    public class TalentExperience
    {
        [Key]
        public Guid Id { get; set; }
        public Guid TalentDetailsId { get; set; }
        public string Year { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Production { get; set; }
        public string? DirectorOrCompany { get; set; }
        public string? Url { get; set; }
       
    }
}
