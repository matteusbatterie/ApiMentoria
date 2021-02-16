using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using ApiMentoria.Service;
using ApiMentoria.Models;
using ApiMentoria.Repository.Interface;

namespace ApiMentoria.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly UserService Service; // usar DI

        public UserController(ILogger<UserController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _userRepository.Retrieve();
        }

        [HttpPost]
        public OkObjectResult Create(User user)
        {
            // Nome tem que ter mais de 10 caracteres
            // Email é válido
            // Email já existe
            // CPF válido
            // CPF já existe
            //Service.Create(user);
            return Ok(Service.Create(user));
        }
    }
}
