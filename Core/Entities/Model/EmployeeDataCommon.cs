using Core.Enums;

namespace Core.Entities
{
    public class EmployeeDataCommon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public decimal Salary { get; set; }
    }
}