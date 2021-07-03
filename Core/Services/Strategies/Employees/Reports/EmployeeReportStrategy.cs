using Core.Enums;

namespace Core.Services.Strategies.Employees.Reports
{
    public abstract class EmployeeReportStrategy
    {
        public UserRole UserRole { get; set; }

        protected EmployeeReportStrategy(UserRole role)
        {
            UserRole = role;
        }

        public abstract EmployeeReportStrategy GetReport(Entities.Employee employee);
    }
}
