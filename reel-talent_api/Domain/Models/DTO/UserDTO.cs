using System.ComponentModel.DataAnnotations;

namespace reel_talent_api.Domain.Models.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; }

        public TalentDTO? Talent { get; set; }
        public CastingProfileDTO? CastingProfile { get; set; }
    }
}
