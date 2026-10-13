using System.ComponentModel.DataAnnotations;

namespace reel_talent_api.Domain.Models.DTO.Requests
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
