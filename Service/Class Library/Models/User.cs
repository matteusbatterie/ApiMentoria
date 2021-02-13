namespace Service.Class_Library.Models
{
    // [Table("User")]
    public class User : BaseEntity
    {
        // [Key]
        // public int Id { get; set; }

        // [Required]
        public string Name { get; set; }
        // [Required]
        public string Email { get; set; }
        // [Required]
        public string Password { get; set; }
        // [Required]
        public string CPF { get; set; }

        public User() { }
    }
}