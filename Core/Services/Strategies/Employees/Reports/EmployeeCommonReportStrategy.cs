using Core.Entities;

namespace Core.Services.Strategies.Employees.Reports
{
    public class EmployeeReportStrategyCommon : EmployeeReportStrategy
    {
        public EmployeeReportStrategyCommon() : base(Enums.UserRole.Common) { }
        private EmployeeReportStrategyCommon(Employee employee) : base(Enums.UserRole.Common)
        {
            Id = employee.Id;
            Name = employee.Name;
            IdUser = employee.IdUser;
        }

        public int Id { get; }
        public string Name { get; }
        public int IdUser { get; }


        public override EmployeeReportStrategy GetReport(Employee employee) =>
            new EmployeeReportStrategyCommon(employee);
    }
}
