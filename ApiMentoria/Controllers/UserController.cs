using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Core.Entities;
using Core.Abstractions.Service;

using Service;

namespace ApiMentoria.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private IUserService _service;

        public UserController(ILogger<UserController> logger, IUserService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public List<User> Get()
        {
            var users = new List<User>();
            users.AddRange(_service.Retrieve());
            return users;
        }

        [HttpPost]
        public OkObjectResult Create(User user)
        {
            return Ok(_service.Create(user));
        }
    }
}
