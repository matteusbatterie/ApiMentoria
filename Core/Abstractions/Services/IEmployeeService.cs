using System.Collections.Generic;
using Core.Entities;
using Core.Enums;
using Core.Services.Strategies.Employees.Reports;

namespace Core.Abstractions.Services
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> Retrieve();
        Employee Retrieve(int id);
        void Create(Employee employee);
        void Update(Employee employee);
        void Delete(int id);

        IEnumerable<EmployeeReportStrategy> GetReports(UserRole role);
    }
}