using SuperORM.TestsResource.Entities;
using SuperORM.WebAPI.DTO.Users;

namespace SuperORM.WebAPI.Domain.Services
{
    public interface IUserService
    {
        IEnumerable<User> Get();
        User Create(CreateUser createUser);
    }
}
