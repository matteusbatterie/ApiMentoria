using System.Linq;
using Core.Entities;

namespace Core.Services.Strategies.Employees.Reports
{
    public class EmployeeAdministratorReportStrategy : EmployeeReportStrategy
    {
        public EmployeeAdministratorReportStrategy() : base(Enums.UserRole.Administrator) { }
        private EmployeeAdministratorReportStrategy(Employee employee) : base(Enums.UserRole.Administrator)
        {
            Id = employee.Id;
            Name = employee.Name;
            CPF = employee.CPF;
            Salary = employee.Salary;
            IdUser = employee.IdUser;
        }

        public int Id { get; }
        public string Name { get; }
        public string CPF { get; set; }
        public decimal Salary { get; set; }
        public int IdUser { get; }


        public override EmployeeReportStrategy GetReport(Employee employee) =>
            new EmployeeAdministratorReportStrategy(employee);
    }
}
