using Core.Enums;

namespace ApiMentoria.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CPF { get; set; }

        public UserRole UserRole { get; set; }

        public UserModel() { }
    }
}