using System.Collections.Generic;
using Service.Class_Library.Models;
using Service.Class_Library.Repository;
using Service.Class_Library.Repository.Interfaces;
using Service.Interfaces;

namespace Service
{
    public class UserService : IUserService/*: BaseService<User>, IUserRepository*/
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) /*: base(userRepository)   */
        { 
            this._userRepository = userRepository;
        }

        public IEnumerable<User> Retrieve()
        {
            return _userRepository.Retrieve();
        }

        public User Retrieve(int id)
        {
            return _userRepository.Retrieve(id);
        }

        public void Create(User user)
        {
            _userRepository.Create(user);
        }

        public void Update(User user)
        {
            _userRepository.Update(user);
        }

        public void Delete(int id)
        {
            _userRepository.Delete(id);
        }
    }
}
