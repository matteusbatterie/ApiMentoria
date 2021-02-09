using ApiMentoria.Class_Library.Interfaces;
using ApiMentoria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.Collections.Generic;

namespace ApiMentoria.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserDAL _userDAL;

        public UserController(ILogger<UserController> logger, IUserDAL userDAL)
        {
            _logger = logger;
            _userDAL = userDAL;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _userDAL.GetAllUsers();  
        }

        [HttpPost]
        public void Create(User user)
        {
            if (ModelState.IsValid)
            {
                // Nome tem que ter mais de 10 caracteres
                // Email é válido
                // Email já existe
                // CPF válido
                // CPF já existe
                _userDAL.AddUser(user);
            }
        }
    }
}
