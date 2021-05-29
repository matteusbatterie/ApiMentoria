using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("Roles")]
    public class Role : BaseEntity
    {
        [Required]
        public string Description { get; set; }
    }
}