using System.Linq;
using System.Collections.Generic;
using Core.Entities;
using Core.Abstractions.Services;
using Core.Abstractions.Repositories;
using System.Text.RegularExpressions;

namespace Core.Services
{
    public class UserService : IUserService
    {
        public readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        
        public IEnumerable<User> Retrieve()
        {
            IEnumerable<User> users = _repository.Retrieve();
            return users;
        }

        public User Retrieve(int id)
        {
            User user = _repository.Retrieve(id);
            return user;
        }

        public void Create(User user)
        {
            if (!ValidateUser(user))
                return;

            _repository.Create(user);
        }

        public void Update(User user)
        {
            if (!ValidateUser(user))
                return;

            _repository.Update(user);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }


        #region Validation
        private bool ValidateUser(User user)
        {
            return NameIsValid(user.Name) &&
                !EmailExists(user.Email) ;
                // !CpfExists(user.CPF) &&
                // CpfIsValid(user.CPF);
        }

        private bool NameIsValid(string name)
        {
            Regex regex = new Regex(@"([a-z])\w+\.([a-z])\w+", RegexOptions.Compiled);
            Match match = regex.Match(name);

            return match.Success;
        }

        private bool EmailExists(string email)
        {
            IEnumerable<User> allUsers = _repository.Retrieve();
            return allUsers.Any(user => user.Email == email);
        }

       
        #endregion
    }
}
