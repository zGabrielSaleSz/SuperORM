using Microsoft.AspNetCore.Mvc;
using SuperORM.TestsResource.Entities;
using SuperORM.WebAPI.Domain.Services;
using SuperORM.WebAPI.DTO.Users;

namespace SuperORM.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _userService.Get();
        }

        [HttpPost]
        public User CreateUser(CreateUser createUser)
        {
            return _userService.Create(createUser);
        }
    }
}