using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ApiMentoria.Models;
using ApiMentoria.Util;
using Core.Entities;
using Core.Abstractions.Services;
using System.Linq;

namespace ApiMentoria.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _service;

        public UserController(ILogger<UserController> logger, IUserService service)
        {
            _logger = logger;
            _service = service;
        }

        #region CRUD
        /// <summary>
        /// Returns all active users in the system.
        /// </summary>
        /// <returns>List of Users</returns>
        [HttpGet]
        public List<UserModel> Get()
        {
            var users = _service.Retrieve().ToList();
            return users.ConvertList<UserModel, User>();
        }

        /// <summary>
        /// Inserts a new User to the system.
        /// </summary>
        /// <param name="model">User object</param>
        [HttpPost]
        public void Create(UserModel model)
        {
            User user = model.Convert<User, UserModel>();
            _service.Create(user);
        }

        /// <summary>
        /// Deletes an user from the database with matching ID.
        /// </summary>
        /// <param name="id">ID from the User to be deleted</param>
        [HttpDelete]
        public void Delete(int id)
        {
            _service.Delete(id);
        }

        /// <summary>
        /// Updates the user with the data given by parameter.
        /// </summary>
        /// <param name="user">Object with the new data and matching ID</param>
        [HttpPut]
        public void Update(UserModel user)
        {
            _service.Update(user.Convert<User, UserModel>());
        }
        #endregion
    }
}
