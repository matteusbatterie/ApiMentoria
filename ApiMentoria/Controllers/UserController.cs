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
        private readonly IUserService _service;

        public UserController(ILogger<UserController> logger, IUserService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _service.Retrieve();
        }

        [HttpPost]
        public void Create(User user)
        {
            _service.Create(user);
        }
    }
}
