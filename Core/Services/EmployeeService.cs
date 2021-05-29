using System.Collections.Generic;
using Core.Abstractions.Repositories;
using Core.Abstractions.Services;
using Core.Entities;

namespace Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        public readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        
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
            _repository.Create(employee);
        }
        
        public void Update(Employee employee)
        {
            _repository.Update(employee);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}