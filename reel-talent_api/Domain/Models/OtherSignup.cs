namespace reel_talent_api.Domain.Models
{
    public class OtherSignup
    {
        public Guid Id { get; set;  } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string Provider { get; set; } = string.Empty;
        public string ProviderUserId { get; set; } = string.Empty;
    }
}
