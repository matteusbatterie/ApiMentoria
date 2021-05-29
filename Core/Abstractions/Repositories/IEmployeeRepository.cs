using System.Collections.Generic;
using Core.Entities;

namespace Core.Abstractions.Repositories
{
    public interface IEmployeeRepository
    {
         IEnumerable<Employee> Retrieve();
         Employee Retrieve(int id);
         void Create(Employee employee);
         void Update(Employee employee);
         void Delete(int id);
    }
}