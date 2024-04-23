using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SitemapGenerator.Models.Entities
{
    public class ProfileEntity
    {
        [Key, ForeignKey("User")]
        public string UserId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? StreeetName { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }

        public UserEntity User { get; set; } = null!;
    }
}
