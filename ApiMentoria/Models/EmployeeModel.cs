namespace ApiMentoria.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }

        public int IdUser { get; set; }


        public UserModel User { get; set; }
    }
}
