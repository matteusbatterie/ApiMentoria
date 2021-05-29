using System.Collections.Generic;
using Core.Abstractions.Services;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiMentoria.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeService _service;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService service)
        {
            _logger = logger;
            _service = service;
        }

        #region CRUD
        /// <summary>
        /// Returns all employees in the system.
        /// </summary>
        /// <returns>List of Employees</returns>
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            var users = _service.Retrieve();
            return users;
        }

        /// <summary>
        /// Inserts a new Employee to the system.
        /// </summary>
        /// <param name="employee">Employee object</param>
        [HttpPost]
        public void Create(Employee employee)
        {
            _service.Create(employee);
        }

        /// <summary>
        /// Deletes an employee from the database with matching ID;
        /// </summary>
        /// <param name="id">ID from the Employee to be deleted</param>
        [HttpDelete]
        public void Delete(int id)
        {
            _service.Delete(id);
        }

        /// <summary>
        /// Updates the employee with the data given by parameter.
        /// </summary>
        /// <param name="employee">Object with the new data and matching ID</param>
        [HttpPut]
        public void Update(Employee employee)
        {
            _service.Update(employee);
        }
        #endregion
    }
}