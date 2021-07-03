using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Enums;

namespace Core.Entities
{
    [Table("Users")]
    public class User : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    
        [Required]  
        public UserRole Role { get; set; }

        public User() { }
    }
}
