using System.ComponentModel.DataAnnotations;

namespace reel_talent_api.Domain.Models.DTO.Requests
{
    public class SignupRequest
    {
        [MaxLength(100)]
        public string? FirstName { get; set; }

        [MaxLength(100)]
        public string? LastName { get; set; }
        public string? CompanyName { get; set; }

        [Required, EmailAddress, MaxLength(254)]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(8)]
        public string Password { get; set; } = string.Empty;

        public UserRole Role { get; set; }
    }
}
