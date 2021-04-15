using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public virtual int Id { get; set; }

        public BaseEntity() { }
    }
}