using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ApiMentoria.Models;
using ApiMentoria.Util;
using Core.Abstractions.Services;
using Core.Entities;
using Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ApiMentoria.Controllers
{
    [Authorize]
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
        public List<EmployeeModel> Get()
        {
            var users = _service.Retrieve().ToList();
            return users.ConvertList<EmployeeModel, Employee>();
        }

        /// <summary>
        /// Inserts a new Employee to the system.
        /// </summary>
        /// <param name="model">Employee object</param>
        [HttpPost]
        public void Create(EmployeeModel model)
        {
            Employee employee = model.Convert<Employee, EmployeeModel>();
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
        /// <param name="model">Object with the new data and matching ID</param>
        [HttpPut]
        public void Update(EmployeeModel model)
        {
            Employee employee = model.Convert<Employee, EmployeeModel>();
            _service.Update(employee);
        }
        #endregion

        /// <summary>
        /// Returns a list of reports from all active employees in the database 
        /// depending on the current user's Role.
        /// </summary>
        /// <returns>Reports from all active employees</returns>
        [HttpGet("Reports")]
        public IActionResult Reports()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var claim =  identity.FindFirst("userRole").Value;
            UserRole role = (UserRole)Enum.Parse(typeof(UserRole), claim);
            var reports = _service.GetReports(role).ToList();
            var result = JsonConvert.SerializeObject(reports);
            return Ok(result);
        }
    }
}
