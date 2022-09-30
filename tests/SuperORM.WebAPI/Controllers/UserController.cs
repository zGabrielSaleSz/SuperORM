using Microsoft.AspNetCore.Mvc;
using SuperORM.Core.Interface.Repository;
using SuperORM.TestsResource.Entities;
using SuperORM.TestsResource.Repositories;

namespace SuperORM.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IRepositoryRegistry _repositoryRegistry;

        public UserController(IRepositoryRegistry repositoryRegistry)
        {
            _repositoryRegistry = repositoryRegistry;
        }
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _repositoryRegistry.GetRepository<UserRepository>()
                .GetSelectable()
                .AsEnumerable();
        }
    }
}