using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SitemapGenerator.Models.Entities
{
    public class ProfileEntity
    {
        [Key, ForeignKey("User")]
        public string UserId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? PhoneNumber { get; set; }

        public UserEntity User { get; set; } = null!;
    }
}
