
using System.ComponentModel.DataAnnotations;

namespace SmartAirPort.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public ICollection<Role>? Roles { get; set; }

        public int? IsActiveate { get; set; } = 1;
        public int? DeActiveateby { get; set; }
        public DateTime DeActiveateOn { get; set; }
        public int IsDeleted { get; set; } = 0;
        public int IsDeleteBy { get; set; }
        public DateTime IsDeleteOn { get; set; }
        public int IsDeleteOnBy { get; set; }
        public int IsUpdated { get; set; } = 0;
        public int IsUpdatedBy { get; set; }
        public DateTime IsUpdatedOn { get; set; }




    }
}