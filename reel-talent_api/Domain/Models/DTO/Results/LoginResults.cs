namespace reel_talent_api.Domain.Models.DTO.Results
{
    public class LoginResults
    {
        public UserDTO User { get; set; }
        public TokenResult Tokens { get; set; }
    }
}
