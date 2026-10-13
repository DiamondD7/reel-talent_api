using System.ComponentModel.DataAnnotations;

namespace reel_talent_api.Domain.Models
{
    public class CastingProfile
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string CompanyName { get; set; } = string.Empty;
    }
}
