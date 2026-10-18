namespace reel_talent_api.Domain.Models.DTO
{
    public class CastingProfileDTO
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public UserDTO? User { get; set; }
        public string? CompanyName { get; set; }
    }
}
