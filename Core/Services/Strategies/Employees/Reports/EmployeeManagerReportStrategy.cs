using System.Linq;
using Core.Entities;

namespace Core.Services.Strategies.Employees.Reports
{
    public class EmployeeManagerReportStrategy : EmployeeReportStrategy
    {
        public EmployeeManagerReportStrategy() : base(Enums.UserRole.Manager) { }
        private EmployeeManagerReportStrategy(Employee employee) : base(Enums.UserRole.Manager)
        {
            Id = employee.Id;
            Name = employee.Name;
            CPF = employee.CPF;
            IdUser = employee.IdUser;
        }

        public int Id { get; }
        public string Name { get; }
        public string CPF { get; set; }
        public int IdUser { get; }


        public override EmployeeReportStrategy GetReport(Employee employee) =>
            new EmployeeManagerReportStrategy(employee);
    }
}
