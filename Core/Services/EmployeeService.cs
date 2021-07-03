using System.Collections.Generic;
using System.Linq;
using Core.Abstractions.Repositories;
using Core.Abstractions.Services;
using Core.Entities;
using Core.Enums;
using Core.Services.Factories.Employees.Reports;
using Core.Services.Strategies.Employees.Reports;

namespace Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        public readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        #region CRUD
        public IEnumerable<Employee> Retrieve()
        {
            IEnumerable<Employee> employees = _repository.Retrieve();
            return employees;
        }

        public Employee Retrieve(int id)
        {
            Employee employee = _repository.Retrieve(id);
            return employee;
        }

        public void Create(Employee employee)
        {
            if (!ValidateEmployee(employee))
                return;

            _repository.Create(employee);
        }

        public void Update(Employee employee)
        {
            if (!ValidateEmployee(employee))
                return;

            _repository.Update(employee);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
        #endregion

        public IEnumerable<EmployeeReportStrategy> GetReports(UserRole role)
        {
            var strategy = EmployeeReportStrategyFactory.GetStrategy(role);
            var employees = _repository
                .Retrieve()
                .Select(employee => strategy.GetReport(employee));

            return employees;
        }

        #region Validation
        private bool ValidateEmployee(Employee employee)
        {
            return !CpfExists(employee.CPF) &&
                    CpfIsValid(employee.CPF);
        }

        private bool CpfExists(string cpf)
        {
            IEnumerable<Employee> allEmployees = _repository.Retrieve();
            return allEmployees.Any(employee => employee.CPF == cpf);
        }

        private bool CpfIsValid(string cpf)
        {
            // Referência: http://www.macoratti.net/11/09/c_val1.htm
            int[] multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;

            string tempCpf = cpf.Substring(0, 9);
            int sum = 0;

            for (int i = 0; i < 9; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];

            int remainder = sum % 11;
            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;

            string digit = remainder.ToString();
            tempCpf = tempCpf + digit;
            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];

            remainder = sum % 11;
            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;

            digit = digit + remainder.ToString();

            return cpf.EndsWith(digit);
        }
        #endregion
    }
}
