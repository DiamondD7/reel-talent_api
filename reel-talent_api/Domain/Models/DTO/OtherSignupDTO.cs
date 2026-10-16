namespace reel_talent_api.Domain.Models.DTO
{
    public class OtherSignupDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public UserDTO? User { get; set; }
        public string Provider { get; set; } = string.Empty;
        public string ProviderUserId { get; set; } = string.Empty;
    }
}
