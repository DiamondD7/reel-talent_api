namespace reel_talent_api.Domain.Models.DTO.Results
{
    public class GooglePayloadResult
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool EmailVerified { get; set; }
        public string? ProviderId { get; set; } //payload.sub 

    }
}
