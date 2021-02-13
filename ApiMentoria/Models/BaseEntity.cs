using System.ComponentModel.DataAnnotations;

namespace ApiMentoria.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public BaseEntity() { }
    }
}