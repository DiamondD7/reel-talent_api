using System.ComponentModel.DataAnnotations;

namespace reel_talent_api.Domain.Models.DTO
{
    public class TalentDTO
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? Role { get; set; }
        public string? Email { get; set; } 
        public TalentDetailsDTO? Details { get; set; }
    }
}
