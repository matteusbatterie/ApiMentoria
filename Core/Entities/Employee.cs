using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("Employees")]
    public class Employee : BaseEntity
    {
        [Required]
        public decimal Salary { get; set; }

        [ForeignKey("User")]
        public int IdUser { get; set; }
    }
}