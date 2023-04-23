using CRMExample.Common;
using CRMExample.DataAccess;
using CRMExample.Entities;
using CRMExample.Models;
using CRMExample.Services.Abstract;
using NETCore.Encrypt.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace CRMExample.Services
{
    public interface IUserService : IServiceBase<User>
    {
        User Authenticate(AuthenticationModel model);
        void Create(CreateUserModel model);


        List<User> List();
        List<User> ListBySearch(string search);
        User GetById(int id);
        void Delete(int id);
        void Update(int id, UpdateUserModel model);

        void ChangePassword(int id, PasswordUserModel model);
        void ChangeUsername(int id, ChangeUsernameModel model);
    }

    public class UserService : ServiceBase<User, IUserRepository>, IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public User Authenticate(AuthenticationModel model)
        {
            model.Username = model.Username.Trim();
            model.Password = (Constants.PasswordSalt + model.Password).MD5();
            return _repository.GetAll(x =>
                    x.Username.ToLower() == model.Username.ToLower() && x.Password == model.Password).FirstOrDefault();
        }


        public void Create(CreateUserModel model)
        {
            string username = model.Username.Trim();

            if (_repository.GetAll(x => x.Username.ToLower() == username).Count == 0)
            {
                User user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    Username = model.Username,
                    Password = (Constants.PasswordSalt + model.Password).MD5(),
                    Role = model.Role,
                    Locked = model.Locked,
                    CreatedAt = System.DateTime.Now
                };

                _repository.Add(user);
            }
            else
            {
                throw new System.Exception("Kullanıcı adı zaten kullanılıyor.");
            }
        }

        public void Update(int id, UpdateUserModel model)
        {
            User user = _repository.Get(id);
            user.Name = model.Name;
            user.Email = model.Email;
            user.Role = model.Role;
            user.Locked = model.Locked;

            _repository.Update(user);
        }

        public void ChangePassword(int id, PasswordUserModel model)
        {
            User user = _repository.Get(id);
            user.Password = (Constants.PasswordSalt + model.Password).MD5();

            _repository.Update(user);
        }

        public List<User> ListBySearch(string search)
        {
            return _repository.GetAll(x =>
                        x.Name.Contains(search) ||
                        x.Email.Contains(search) ||
                        x.Role.Contains(search) ||
                        x.Username.Contains(search));
        }

        public void ChangeUsername(int id, ChangeUsernameModel model)
        {
            string username = model.Username.Trim();

            if (_repository.GetAll(x => x.Username.ToLower() == username && x.Id != id).Count == 0)
            {
                User user = _repository.Get(id);
                user.Username = model.Username;

                _repository.Update(user);
            }
            else
            {
                throw new System.Exception("Kullanıcı adı zaten kullanılıyor.");
            }
        }

    }
}
