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

        public string? Password { get; set; }  //nullable for when user uses google or other providers.
        public string? ProviderId { get; set; }
        public bool OtherProvider { get; set; } = false; //if true then its Google, if false then traditional password email combo

        public UserRole Role { get; set; }
    }
}
