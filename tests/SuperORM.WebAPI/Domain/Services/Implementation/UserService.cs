using SuperORM.TestsResource.Entities;
using SuperORM.WebAPI.Domain.Util;
using SuperORM.WebAPI.DTO.Users;
using SuperORM.WebAPI.Infrastructure.MySqlImp;

namespace SuperORM.WebAPI.Domain.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly UnityOfWork _unityOfWork;

        public UserService(UnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        public IEnumerable<User> Get()
        {
            return _unityOfWork.Users
                .GetSelectable()
                .AsEnumerable();
        }


        public User Create(CreateUser createUser)
        {
            PasswordHandler.Check(createUser.Password);
            _unityOfWork.UseTransaction();

            User userToInsert = new User
            {
                Name = createUser.Name,
                email = createUser.Email,
                password = createUser.Password,
                active = true,
                approvedDate = DateTime.Now
            };

            _unityOfWork.Users.Insert(userToInsert);
            _unityOfWork.Commit();

            return userToInsert;
        }
    }
}
