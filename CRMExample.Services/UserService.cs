using CRMExample.DataAccess;
using CRMExample.Entities;
using CRMExample.Models;
using System.Linq;

namespace CRMExample.Services
{
    public interface IUserService
    {
        User Authenticate(AuthenticationModel model);
        void Create(CreateUserModel model);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public User Authenticate(AuthenticationModel model)
        {
            model.Username = model.Username.Trim();
            return _repository.GetAll(x =>
                    x.Username.ToLower() == model.Username.ToLower() && x.Password == model.Password).FirstOrDefault();
        }

        public void Create(CreateUserModel model)
        {
            User user = new User
            {
                Name = model.Name,
                Email = model.Email,
                Username = model.Username,
                Locked = model.Locked,
                CreatedAt = System.DateTime.Now
            };
            _repository.Add(user);
        }
    }


}
