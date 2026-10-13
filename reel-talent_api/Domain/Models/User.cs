using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel.DataAnnotations;

namespace reel_talent_api.Domain.Models
{
    public enum UserRole { Talent, Casting }
    public class User
    {
        public Guid Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; }

        public Talent? Talent { get; set; }
        public CastingProfile? CastingProfile { get; set; }
    }
}
