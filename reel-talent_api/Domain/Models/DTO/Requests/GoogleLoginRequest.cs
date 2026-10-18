namespace reel_talent_api.Domain.Models.DTO.Requests
{
    public class GoogleLoginRequest
    {
        public string IdToken { get; set; } = string.Empty;
        public UserRole? Role { get; set; }
    }
}
