namespace reel_talent_api.Domain.Models.DTO.Results
{
    public class TokenResult
    {
        public string AccessToken { get; set; }
        public string RawRefreshToken { get; set; }
        public DateTime AccessTokenExpiresAt { get; set; }
        public DateTime RefreshTokenExpiresAt { get; set; }
    }
}
