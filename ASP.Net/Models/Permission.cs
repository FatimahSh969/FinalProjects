using System.ComponentModel.DataAnnotations;
using System.Data;

namespace SmartAirPort.Models
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Role>? Roles { get; set; }
    }
}
